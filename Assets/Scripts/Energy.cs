using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public float StartingEnergy=100;
	public float CurrentEnergy;

	// Use this for initialization
	void Start () {
		CurrentEnergy = StartingEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		//Destrukce při nulové energii
		if (GetComponent<Energy> ().CurrentEnergy <= 0) {
			Destroy ();
		}
	}
	
	void Destroy(){
		Destroy (gameObject);
	}
}
