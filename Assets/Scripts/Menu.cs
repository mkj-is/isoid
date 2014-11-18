using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void StartGame() {
		Application.LoadLevel("Prototype");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
