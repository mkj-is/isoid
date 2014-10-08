﻿using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public float rotationSpeed = 30f;
	
	void FixedUpdate () {

		transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
	}
}
