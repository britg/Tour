using UnityEngine;
using System.Collections;

public class PlayerController : GameController {

	public Player player;

	// Use this for initialization
	void Start () {
		player.go = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
