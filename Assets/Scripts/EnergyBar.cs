using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {
	Vector2 pos = new Vector2(60, 40);
	Vector2 size = new Vector2(200, 30);
	
	float barFill = 0.8f;
	string barText = "";

	public GameObject Player;
    public GameObject Joystick;
	
	public GUIStyle progress_empty;
	public GUIStyle progress_full;

	void OnGUI () {
        if (Joystick.activeInHierarchy && Screen.width <= 800)
        {
            pos.x = Screen.width - size.x - 60;
            pos.y = 80;
        }
        else if (Joystick.activeInHierarchy)
        {
            pos.x = 200;
            pos.y = 40;
        }
        
        GUI.BeginGroup(new Rect(pos.x, Screen.height - size.y - pos.y, size.x, size.y));

        // Draw the background and fill
        GUI.Box(new Rect(0, 0, size.x, size.y), barText, progress_empty);
        GUI.Box(new Rect(0, 0, size.x * barFill, size.y), barText, progress_full);

        GUI.EndGroup();
	}
	
	void Update () {
		barFill = Player.GetComponent<Energy> ().CurrentEnergy / Player.GetComponent<Energy> ().StartingEnergy;
	}
}