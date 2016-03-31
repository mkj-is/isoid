using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

    public float forwardSpeed = 6f;
    public float rotationSpeed = 10f;
    public float height = 2f;

	public AudioSource pickupSound;

    public GameObject Joystick;

    Rigidbody playerRigidBody;
    ParticleSystem exhaustParticles;



    void Awake()
    {
        Time.timeScale = 1;

        playerRigidBody = GetComponent<Rigidbody>();
        exhaustParticles = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        float h = 0;
        float v = 0;

        if (!Joystick.activeInHierarchy)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            v = Vector2.SqrMagnitude(new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical")));

            float InputRotation = Mathf.Atan2(-CrossPlatformInputManager.GetAxisRaw("Horizontal"), -CrossPlatformInputManager.GetAxisRaw("Vertical")) * Mathf.Rad2Deg + 180;
            float HoverRotation = transform.rotation.eulerAngles.y;
            float RotationalDifference = Mathf.Abs(InputRotation - HoverRotation) > 180 ? HoverRotation - InputRotation : InputRotation - HoverRotation;

            if (v > 0.1 && Mathf.Abs(RotationalDifference) > 5)
            {
                if (RotationalDifference > 0)
                    h = 1;
                else
                    h = -1;
            }
        }

        Move(v);

        Turning(h);

        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        ParticleSystem.EmissionModule em = exhaustParticles.emission;
        em.rate = new ParticleSystem.MinMaxCurve(2 + v * forwardSpeed / 30);
        //exhaustParticles.emissionRate = 2 + v * forwardSpeed / 30;
    }

    void Move(float v)
    {
        playerRigidBody.AddForce(transform.forward * v * Time.deltaTime * forwardSpeed);
    }

    void Turning(float h)
    {
        playerRigidBody.AddTorque(Vector3.up * rotationSpeed * Time.deltaTime * h);
    }
	
    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.name == "Isoid(Clone)")
        {
			pickupSound.Play();
            float energyLeft = other.GetComponent<Energy>().CurrentEnergy;
            this.GetComponent<Energy>().CurrentEnergy += energyLeft;
			ScoreManager.score += energyLeft;
			this.GetComponent<Shooting>().currentEnergy = this.GetComponent<Shooting>().startingEnergy;
            Destroy(other.gameObject);
        }
		else if (other.gameObject.name == "PowerupNocost(Clone)")
        {
            this.GetComponent<Shooting>().nocostPickup();

            Destroy(other.gameObject);
        }
		else if (other.gameObject.name == "PowerupFirerate(Clone)")
        {
            this.GetComponent<Shooting>().fireratePickup();

            Destroy(other.gameObject);
        }
		else if (other.gameObject.name == "PowerupSplit(Clone)")
        {
            this.GetComponent<Shooting>().splitPickup();

            Destroy(other.gameObject);
        }
    }
}
