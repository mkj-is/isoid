using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	public float speed = 1f;
	//protected float multiplier = 0.5f;
	//public float maxSize = 1.0f;
	//public float minSize = 0.5f;

    protected float counter = 0;
    protected float multiplier;
    public float averageSize = 0.5f;
    public float pulseSize = 0.2f;

	// Update is called once per frame
	void FixedUpdate () {
		//transform.localScale.Scale(new Vector3 (speed, speed, speed));
		//if (Time.realtimeSinceStartup % 3 == 0) {

        /*
		multiplier += Time.deltaTime * speed;
		transform.localScale = Vector3.one * multiplier;
		if (multiplier > maxSize) {
			multiplier = minSize;
		}
        */

        counter += Time.deltaTime * speed;
        multiplier = (Mathf.Sin(counter) * pulseSize) + averageSize;
        transform.localScale = Vector3.one * multiplier;
        if (counter > 180)
            counter = 0;

	}
}
