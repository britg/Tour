using UnityEngine;
using System.Collections;

public class DeathController : GameController {

	Player player;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == GameLayer.Player) {
			player.fallen = true;
		}
	}
}
