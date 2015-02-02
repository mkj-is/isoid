using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static float score;

	// Use this for initialization
	void Awake () {
        score = 0f;
	}

	void FixedUpdate () {
		score += 0.1f;
	}
}
