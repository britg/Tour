using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public GameObject obstaclePrefab;
	public float startZ = 185f;

	public float minSpawnTime;
	public float maxSpawnTime;

	float nextSpawnTime;
	float currentSpawnTime;

	Player.Lane nextSpawnLane;

	// Use this for initialization
	void Start () {
		Randomize();
	}

	void Randomize () {
		currentSpawnTime = 0f;
		nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		float r = Random.value;
		if (r < 0.5f) {
			nextSpawnLane = Player.Lane.Left;
		} else {
			nextSpawnLane = Player.Lane.Right;
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentSpawnTime += Time.deltaTime;
		if (currentSpawnTime >= nextSpawnTime) {
			Spawn();
			Randomize();
		}
	}

	void Spawn () {
		Vector3 pos = new Vector3(0f, 1f, startZ);
		if (nextSpawnLane == Player.Lane.Left) {
			pos.x = 2.5f;
		} else {
			pos.x = -2f;
		}
		GameObject ob = (GameObject)Instantiate(obstaclePrefab, pos, Quaternion.identity);
	}
}
