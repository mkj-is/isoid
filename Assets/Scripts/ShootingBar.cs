﻿using UnityEngine;
using System.Collections;

public class ShootingBar : MonoBehaviour {
	Vector2 pos = new Vector2(60, 40);
	Vector2 size = new Vector2(200, 30);
	
	float barFill = 0.8f;
	string barText = "";

	public GameObject Player;
	
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	
	void OnGUI () {
		// Create a group container to make positioning easier
		GUI.BeginGroup(new Rect(Screen.width - size.x - pos.x, Screen.height - size.y - pos.y, size.x, size.y));
		
		// Draw the background and fill
		GUI.Box(new Rect(0, 0, size.x, size.y), barText, progress_empty);
		GUI.Box(new Rect(0, 0, size.x * barFill, size.y), barText, progress_full);
		
		GUI.EndGroup();
	}
	
	void Update () {
		barFill = Player.GetComponent<Shooting> ().currentEnergy / Player.GetComponent<Shooting> ().startingEnergy;
	}
}