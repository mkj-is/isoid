using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

	public float magnitude = 5.0f;
	private Vector3 rotation;

	// Use this for initialization
	void Start () {
		rotation = new Vector3 (Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), Random.Range(magnitude, magnitude));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.Rotate (rotation);
	}
}
