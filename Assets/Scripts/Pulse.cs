using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	public float speed = 0.001f;
	protected float multiplier = 0.5f;
	public float maxSize = 1.0f;
	public float minSize = 0.5f;
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.localScale.Scale(new Vector3 (speed, speed, speed));
		//if (Time.realtimeSinceStartup % 3 == 0) {
		multiplier += Time.deltaTime * speed;
		transform.localScale = Vector3.one * multiplier;
		if (multiplier > maxSize) {
			multiplier = minSize;
		}


	}
}
