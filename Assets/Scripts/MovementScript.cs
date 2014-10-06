using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

    public float speed;

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3(rigidbody.position.x, 5f, rigidbody.position.z);
    }

}
