using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public float StartingEnergy=100;
	public float CurrentEnergy;

    public float energyDecreaseRate = 1f;

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

    void FixedUpdate()
    {
        if (this.gameObject.tag == "Player")
        {
            CurrentEnergy -= energyDecreaseRate * Time.deltaTime * Mathf.Abs(Input.GetAxisRaw("Vertical"));
        }
        else
        {
            CurrentEnergy -= energyDecreaseRate * Time.deltaTime;
        }
    }

	void Destroy(){
		Destroy (gameObject);
	}
}
