using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GameObject scoreTextObject;
	public GameObject restartTextObject;
	public GameObject gameOverTextObject;
	
	int score;
	bool gameOver;
	bool restart;
	
	Text scoreText;
    Text restartText;
	Text gameOverText;

	void Start()
	{
		scoreText = scoreTextObject.GetComponent<Text>();
		restartText = restartTextObject.GetComponent<Text>();
		gameOverText = gameOverTextObject.GetComponent<Text>();
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves());
	}

	void Update ()
	{
		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition =  new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);		
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}
    
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver ()
	{
		gameOverText.text =  "Game Over!";
		gameOver = true;
	}
}
