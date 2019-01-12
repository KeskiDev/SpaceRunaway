using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWaitTime;
	public float startWait;
	public float waveWait;

	public TextMesh gameOverText;
	public TextMesh restartText;
	public TextMesh scoreText;
	private int score;
	private bool gameOver;
	private bool restart;

	private void Start()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	private void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				var currentScene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(currentScene.name);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWaitTime);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' to restart the game.";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
