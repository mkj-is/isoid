using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

	public float DestroyTime = 2f;
	public float DestructionForce = 30f;

	// Update is called once per frame
	void Awake () {
		Destroy (gameObject, DestroyTime);
	}
}
