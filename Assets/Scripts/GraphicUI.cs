using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GraphicUI : MonoBehaviour {

	public GameObject Player;
	private bool gameOver;
	private float endTime;
	public GUIStyle guiLay;
    private float finalScore;

    private float inputTimer;

	void Start(){
		Cursor.visible = true;
		gameOver = false;
	}

	void OnGUI()
	{
		if (!gameOver) {
			GUI.Label (new Rect (Screen.width - 200 - 50, 40, 200, 50), "SCORE: " + ScoreManager.score.ToString ("00000"), guiLay);
		} else {
			guiLay.fontSize=35;
			GUI.Label (new Rect ((Screen.width/2) - 100,(Screen.height/2)-100,200,80),"GAME OVER", guiLay);
			guiLay.fontSize=25;
			GUI.Label (new Rect ((Screen.width/2) - 100,(Screen.height/2),200,80),"TIME: "+endTime.ToString("F1") + " S",guiLay);
            guiLay.fontSize = 25;
			GUI.Label(new Rect((Screen.width / 2) - 100, (Screen.height / 2) + 100, 200, 80), "SCORE: " + finalScore.ToString ("00000"), guiLay);
		}
	}
	void Update(){
		if(Input.GetKey(KeyCode.Escape) || (gameOver && Input.anyKeyDown && Input.GetAxis ("Fire1") != 1 && (Time.time-inputTimer > 2))){
			Cursor.visible = true;
            SceneManager.LoadScene("Menu");
			//Application.LoadLevel("Menu");
		}
	}

	public void EndGame(){
		gameOver = true;
		endTime = Time.timeSinceLevelLoad;
        finalScore = ScoreManager.score;
        StoreHighScore(finalScore);
        inputTimer = Time.time;
	}

    void StoreHighScore(float newScore)
    {
        float oldHighscore = PlayerPrefs.GetFloat("highscore", 0f);
        if (newScore > oldHighscore)
        {
            PlayerPrefs.SetFloat("highscore", newScore);
        }
    }
}
