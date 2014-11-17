using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject missile;
	public float energyCosts = 5f;
    public float fireRate = 0.2f;

    private float nextFire;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("v") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
			Fire ();
			GetComponent<Energy>().CurrentEnergy -=energyCosts;
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
