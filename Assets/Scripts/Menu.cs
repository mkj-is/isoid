using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    void Start()
    {
        GameObject.Find("Highscore").GetComponent<Text>().text = "Highscore: " + PlayerPrefs.GetFloat("highscore", 0).ToString("0");
    }

	public void StartGame() {
		Application.LoadLevel("Prototype");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
