using UnityEngine;
using System.Collections;

public abstract class GameController : GameBehaviour {

	protected void Move (Player player) {
		if (player.fallen) {
			return;
		}
		var newPos = transform.position + Vector3.back * Time.deltaTime * player.currentSpeed / 5f;
		transform.position = newPos;
	}

}
