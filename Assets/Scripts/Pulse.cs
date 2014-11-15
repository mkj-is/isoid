using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	public float speed = 1f;

    protected float counter = 0;
    protected float multiplier;
    public float averageSize = 0.5f;
    public float pulseSize = 0.2f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void FixedUpdate () {
        counter += Time.deltaTime * speed;
        multiplier = (Mathf.Sin(counter) * pulseSize) + averageSize;
        transform.localScale = Vector3.one * multiplier;
        if (counter > 180)
            counter = 0;

        if (Vector3.Distance(this.transform.position, player.transform.position) > 1000)
        {
            Destroy(this.gameObject);
        }
	}
}
