using UnityEngine;
using System.Collections;

public class AsteroidCollisions : MonoBehaviour {

	public int collisionDamage = 1000;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Asteroid")
		{
			this.rigidbody.velocity = -this.rigidbody.velocity;
		}
		if (other.tag == "Missile")
		{
			Destroy(other.gameObject);
		}
		if (other.tag == "Enemy" || other.tag == "Player")
		{
			other.gameObject.GetComponent<Energy>().CurrentEnergy -= collisionDamage;
		}
	}
}
