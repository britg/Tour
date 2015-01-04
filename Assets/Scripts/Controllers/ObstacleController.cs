using UnityEngine;
using System.Collections;

public class ObstacleController : GameController  {

	public Obstacle obstacle;
	Player player;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		obstacle.go = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Move(player);
		Cull();
	}

	void Cull () {
		if (transform.position.z < - 15f) {
			Destroy(gameObject);
		}
	}
}
