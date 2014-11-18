using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float forwardSpeed = 6f;
    public float rotationSpeed = 10f;
    public float height = 2f;

    Rigidbody playerRigidBody;
    ParticleSystem exhaustParticles;

    void Awake()
    {
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

        exhaustParticles.emissionRate = 2 + v * v * forwardSpeed / 2;
        /*if (v > 0)
        {
            exhaustParticles.enableEmission = true;
        }
        else
        {
            exhaustParticles.enableEmission = false;
        }*/
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
            float energyLeft = other.GetComponent<Energy>().CurrentEnergy;
            this.GetComponent<Energy>().CurrentEnergy += energyLeft;
            Destroy(other.gameObject);
        }
    }
}
