using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {

	public float rotationMagnitude = 5.0f;
	public float speed = 5.0f;
	public float randomScale = 0.3f;
	private Vector3 rotation;
	private Vector3 direction;
	
	// Use this for initialization
	void Start () {
		rotation = new Vector3 (Random.Range(-rotationMagnitude, rotationMagnitude), Random.Range(-rotationMagnitude, rotationMagnitude), Random.Range(rotationMagnitude, rotationMagnitude));
		direction = new Vector3 (Random.Range(-speed, speed), 0.0f, Random.Range(-speed, speed));
		GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime * speed);
		float scale = Random.Range (1f - randomScale, 1f + randomScale);
		transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (rotation);
	}
}
