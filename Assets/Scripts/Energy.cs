using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public float StartingEnergy=100;
	public float CurrentEnergy;

    public float energyDecreaseRate = 1f;

    public int score = 0;

    public GameObject explosion;

    private float DefaultEnergy;
    private float DefaultEnergyDecreaseRate;
    private bool cheat = false;

	// Use this for initialization
	void Start () {
		CurrentEnergy = StartingEnergy;
        DefaultEnergy = StartingEnergy;
        DefaultEnergyDecreaseRate = energyDecreaseRate;
	}
	
	// Update is called once per frame
	void Update () {

		if (CurrentEnergy > StartingEnergy) {
			CurrentEnergy = StartingEnergy;
		}

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
		if (this.gameObject.tag == "Player") {
			gameObject.SetActive(false);

            Object.Destroy(Instantiate(explosion, this.transform.position, this.transform.rotation), 10);

            GameObject.Find("GUI").GetComponent<GraphicUI>().EndGame();
        }
        else if (this.gameObject.tag == "Enemy")
        {
            //Instantiate and destroy explosion
            Object.Destroy(Instantiate(explosion, this.transform.position, this.transform.rotation), 10);

            ScoreManager.score += score;
            Destroy(gameObject);
        }
        else {
			Destroy (gameObject);
		}
	}


    void OnIddqdCode()
    {
        if (!cheat)
        {
            Debug.Log("iddqd activated!");
            cheat = true;
            StartingEnergy = float.MaxValue;
            CurrentEnergy = StartingEnergy;
            energyDecreaseRate = 0f;
        }
        else
        {
            Debug.Log("iddqd deactivated!");
            cheat = false;
            StartingEnergy = DefaultEnergy;
            CurrentEnergy = StartingEnergy;
            energyDecreaseRate = DefaultEnergyDecreaseRate;
        }
    }
}
