using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject missile;
	public float energyCosts = 5f;
    public float fireRate = 0.2f;
	public float startingEnergy = 100f;
	public float currentEnergy = 100f;
	public float refillRate = 0.5f;

    private float nextFire;

	void OnStart() {
		currentEnergy = startingEnergy;
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetAxis ("Fire1")==1 ) && Time.time > nextFire && currentEnergy - energyCosts > 0f) {
            nextFire = Time.time + fireRate;
			Fire ();
			currentEnergy -=energyCosts;
		}
	}

	void FixedUpdate() {
		currentEnergy += refillRate;
		if (currentEnergy > startingEnergy) {
			currentEnergy = startingEnergy;
		}
	}

	void Fire(){
        Instantiate(missile, transform.FindChild("Canon Head").transform.position, transform.FindChild("Canon Head").transform.rotation);

        //GameObject newMissile = Instantiate (Missile) as GameObject;
        //newMissile.transform.position = transform.FindChild ("Canon Head").transform.position;
        //newMissile.transform.rotation = transform.FindChild ("Canon Head").transform.rotation;

		//newMissile.GetComponent<Rigidbody> ().velocity = rigidbody.velocity;
		//newMissile.GetComponent<Rigidbody> ().AddForce (transform.forward * 200000 * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            GetComponent<Energy>().CurrentEnergy -= other.GetComponent<MissileScript>().DestructionForce / 2;
            Destroy(other.gameObject);
        }
    }
}
