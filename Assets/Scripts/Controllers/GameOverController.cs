using UnityEngine;
using System.Collections;

public class GameOverController : GameController {

	Player player;
	public GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		gameOverPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.fallen) {
			player.Save();
			gameOverPanel.SetActive(true);
		}
	}

	public void OnRestarButtonPress () {
		Restart();
	}

	void Restart () {
		Application.LoadLevel(0);
	}
}
