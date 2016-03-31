using UnityEngine;
using System.Collections;

public class PowerupBar : MonoBehaviour {
    Vector2 pos = new Vector2(60, 40);
    Vector2 size = new Vector2(60, 30);

    private float nocostBarFill = 1f;
    private float firerateBarFill = 1f;
    private float splitBarFill = 1f;
    string barText = "";

    public GameObject Player;
    public GameObject Joystick;

    public GUIStyle nocost_full;
    public GUIStyle firerate_full;
    public GUIStyle split_full;

    void OnGUI()
    {
        // Create a group container to make positioning easier
        if (Joystick.activeInHierarchy)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - size.x / 2 + 70, Screen.height - size.y - pos.y, size.x, size.y));
        }
        else
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - size.x / 2, Screen.height - size.y - pos.y, size.x, size.y));
        }

        // Nocost
        GUI.Box(new Rect(0, size.y - size.y * nocostBarFill, 15, size.y * nocostBarFill), barText, nocost_full);

        // Firerate
        GUI.Box(new Rect(20, size.y - size.y * firerateBarFill, 15, size.y * firerateBarFill), barText, firerate_full);

        // Split
        GUI.Box(new Rect(40, size.y - size.y * splitBarFill, 15, size.y * splitBarFill), barText, split_full);

        GUI.EndGroup();
    }

    void Update()
    {
        nocostBarFill = Player.GetComponent<Shooting>().nocostRemaining / Player.GetComponent<Shooting>().nocostDuration;
        firerateBarFill = Player.GetComponent<Shooting>().firerateRemaining / Player.GetComponent<Shooting>().firerateDuration;
        splitBarFill = Player.GetComponent<Shooting>().splitRemaining / Player.GetComponent<Shooting>().splitDuration;
    }
}
