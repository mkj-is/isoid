using UnityEngine;
using System.Collections;

public class UfoManager : MonoBehaviour {

    public float spawnTime = 3f;
    public float spawnDistance = 20f;
    public float spawnRandomDistance = 5f;
    public GameObject basicEnemy;
	public GameObject mediumEnemy;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    void Spawn()
    {
		GameObject enemy = basicEnemy;
		if (Time.timeSinceLevelLoad > 15.0 && Random.Range (0.0f, 1.0f) > 0.5f) {
			enemy = mediumEnemy;
		}
        Instantiate(enemy, RandomCircle(player.transform.position, Random.Range(spawnDistance - spawnRandomDistance, spawnDistance + spawnRandomDistance)), Quaternion.Euler(-90, 0, 0));
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        int angle = Random.Range(0, 360);
        Vector3 position;
        position.x = center.x + (radius * Mathf.Cos(Mathf.Deg2Rad * angle));
        position.y = center.y;
        position.z = center.z + (radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        return position;
    }
}
