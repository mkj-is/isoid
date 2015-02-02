using UnityEngine;
using System.Collections;

public class GraphicUI : MonoBehaviour {

	public GameObject Player;
	private bool gameOver;
	private float endTime;
	public GUIStyle guiLay;
    private int finalScore;

	void Start(){
		gameOver = false;
	}

	void OnGUI()
	{
		if (!gameOver) {
			//GUI.Label (new Rect (25, 25, 100, 30), Player.GetComponent<Energy> ().CurrentEnergy.ToString ("F2"));
			//GUI.Label (new Rect (25, 55, 100, 30), Time.realtimeSinceStartup.ToString ("F1"));
		} else {
			guiLay.fontSize=35;
			GUI.Label (new Rect ((Screen.width/2)-100,(Screen.height/2)-100,200,80),"GAME OVER", guiLay);
			guiLay.fontSize=25;
			GUI.Label (new Rect ((Screen.width/2)-50,(Screen.height/2),200,80),"Time: "+endTime.ToString("F1"),guiLay);
            guiLay.fontSize = 25;
            GUI.Label(new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 100, 200, 80), "Score: " + finalScore, guiLay);
		}
	}
	void Update(){
		if (gameOver) {
			if(Input.anyKeyDown){
				Application.LoadLevel("Menu");
			}
		}
	}

	public void EndGame(){
		gameOver = true;
		endTime = Time.realtimeSinceStartup;
        finalScore = ScoreManager.score;
        //Time.timeScale = 0;
	}
}
