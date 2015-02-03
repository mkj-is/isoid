using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float forwardSpeed = 6f;
    public float rotationSpeed = 10f;
    public float height = 2f;

	public AudioSource pickupSound;

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

        if (Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).position.x < Screen.width / 2)
                {
                    h = -1;
                }
                else
                {
                    h = 1;
                }
            }
            else if (Input.touchCount >= 2)
            {
                v = 1;
            }
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        Move(v);

        Turning(h);

        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        exhaustParticles.emissionRate = 2 + v * forwardSpeed / 30;
    }

    void Move(float v)
    {
        //playerRigidBody.MovePosition(position + movement);
        playerRigidBody.AddForce(transform.forward * v * Time.deltaTime * forwardSpeed);
    }

    void Turning(float h)
    {
        playerRigidBody.AddTorque(Vector3.up * rotationSpeed * Time.deltaTime * h);
        //playerRigidBody.WakeUp ();
        //transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * h);
    }

    //Provizorní --> musí se změnit
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Isoid")
        {
			pickupSound.Play();
            float energyLeft = other.GetComponent<Energy>().CurrentEnergy;
            this.GetComponent<Energy>().CurrentEnergy += energyLeft;
			ScoreManager.score += energyLeft;
			this.GetComponent<Shooting>().currentEnergy = this.GetComponent<Shooting>().startingEnergy;
            Destroy(other.gameObject);
        }
        else if (other.tag == "PowerupNocost")
        {
            this.GetComponent<Shooting>().nocostPickup();

            Destroy(other.gameObject);
        }
        else if (other.tag == "PowerupFirerate")
        {
            this.GetComponent<Shooting>().fireratePickup();

            Destroy(other.gameObject);
        }
        else if (other.tag == "PowerupSplit")
        {
            this.GetComponent<Shooting>().splitPickup();

            Destroy(other.gameObject);
        }
    }
}
