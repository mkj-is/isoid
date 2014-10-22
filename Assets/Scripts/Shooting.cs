using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject Missile;
	public float EnergyCosts = 5f;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("v")) {
			Fire ();
			GetComponent<Energy>().CurrentEnergy -=EnergyCosts;
		}
	}

	void Fire(){
		GameObject newMissile = Instantiate (Missile) as GameObject;
		newMissile.transform.position = transform.FindChild ("Canon Head").transform.position;
		newMissile.transform.rotation = transform.FindChild ("Canon Head").transform.rotation;

		newMissile.GetComponent<Rigidbody> ().velocity = rigidbody.velocity;
		newMissile.GetComponent<Rigidbody> ().AddForce (transform.forward * 20000 * Time.deltaTime);
	}
}
