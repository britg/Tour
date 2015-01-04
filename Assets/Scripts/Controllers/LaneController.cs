using UnityEngine;
using System.Collections;

public class LaneController : GameController {

	Player player;
	public Player.Lane targetLane;

	public Vector3 rightLane;
	public Vector3 leftLane;
	public float switchTime = 0.2f;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		DetectInput();
		if (player.currentLane != targetLane) {
			TransitionToLane(targetLane);
			player.currentLane = targetLane;
		}
	}

	void TransitionToLane (Player.Lane lane) {
		if (lane == Player.Lane.Right) {
			TransitionRight();
		} else {
			TransitionLeft();
		}
	}

	void TransitionRight () {
		iTween.MoveTo(gameObject, iTween.Hash("position", rightLane, "time", switchTime));
	}

	void TransitionLeft () {
		iTween.MoveTo(gameObject, iTween.Hash("position", leftLane, "time", switchTime));
	}

	void DetectInput () {
		if (Input.GetKeyDown(KeyCode.UpArrow) || 
		    (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)) {
			SwitchLanes();
		}
	}

	void SwitchLanes () {
		if (player.currentLane == Player.Lane.Right) {
			targetLane = Player.Lane.Left;
		} else {
			targetLane = Player.Lane.Right;
		}
	}

}
