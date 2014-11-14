using UnityEngine;
using System.Collections;

public class UFOMover : MonoBehaviour {
	
	public float rotationSpeed = 30f;
	public float wobbleSpeed = 2f;
	public float wobbleMagnitude = 0.001f;
	
	void FixedUpdate () {
		//transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
		//transform.Translate(new Vector3(0, 0, Mathf.Sin(Time.realtimeSinceStartup * wobbleSpeed)) * wobbleMagnitude);

	}


	void OnTriggerEnter(Collider other){
		if (other.tag == "Missile") {
			GetComponent<Energy>().CurrentEnergy -= other.GetComponent<MissileScript>().DestructionForce;
			Destroy(other.gameObject);
		}
	}
}
