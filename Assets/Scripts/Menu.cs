using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    void Start()
    {
        GameObject.Find("Highscore").GetComponent<Text>().text = "HIGHSCORE: " + PlayerPrefs.GetFloat("highscore", 0).ToString("00000");
    }

	void Update()
	{
		if(Input.GetKey(KeyCode.Escape)){
			ExitGame();
		} else if(Input.GetAxis ("Fire1")==1){
			StartGame();
		}
	}

	public void StartGame() {
        SceneManager.LoadScene("Prototype");
		//Application.LoadLevel("Prototype");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
