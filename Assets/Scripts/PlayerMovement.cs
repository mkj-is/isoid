using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float forwardSpeed = 6f;
    public float rotationSpeed = 10f;
    public float height = 3f;

    Vector3 movement;
    Rigidbody playerRigidBody;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(v);

        Turning(h);
    }

    void Move(float v)
    {
        movement = transform.forward * forwardSpeed * Time.deltaTime * v;
        Vector3 position = transform.position;
        position.y = height;

        playerRigidBody.MovePosition(position + movement);
    }

    void Turning(float h)
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * h);
    }
}
