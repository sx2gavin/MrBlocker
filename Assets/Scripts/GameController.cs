using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Text infoText;
	public Text scoreText;
	public Text gameOverText;
	public Text timeText;

	int rayNumber;
	bool gameOver = false;
	AudioSource booSound;
	float startTime;


	// Use this for initialization
	void Start () {
		rayNumber = 0;

		booSound = GetComponents<AudioSource> () [0];
		infoText.text = "Welcome to MrBlocker. \n\nThe goal of this game is to protect the piggy from red objects. \nUse WSAD to move piggy around and left click to block.\n Enemy will duplicate after touching your blocks so BEWARE!";
		startTime = Time.time;
		gameOverText.text = "";
		timeText.text = "";
	}	
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		} else {
			scoreText.text = "Time: " + (Time.time - startTime).ToString("F2") + " seconds";
		}
	}

	public bool CanMakeRay()
	{
		if (rayNumber < 4) {
			return true;
		} else {
			return false;
		}
	}

	public void AddRay()
	{
		rayNumber = rayNumber + 1;
	}

	public void RayDestroyed()
	{
		rayNumber = rayNumber - 1;
	}

	public void GameOver()
	{
		gameOver = true;
		infoText.text = "Press 'R' to restart the Game\n";
		gameOverText.text = "Game Over!\nYou protected our Piggy for:\n";
		timeText.text = (Time.time - startTime).ToString("F2") + " seconds!";
		booSound.Play ();
	}
}
