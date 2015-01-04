using UnityEngine;
using System.Collections;

public class RoadController : GameController {

	Player player;

	public GameObject stripePrefab;
	public float spacing;
	public float resetZ;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		LayStripes();
	}
	
	// Update is called once per frame
	void Update () {
		Move(player);
	}

	void LateUpdate () {
		Reset();
	}

	void LayStripes () {
		for (int i = 0; i < 50; i++) {
			float z = i * spacing + transform.position.z;
			GameObject stripe = (GameObject)Instantiate(stripePrefab);
			stripe.transform.position = new Vector3(0.5f, 0.6f, z);
			stripe.transform.SetParent(transform);
		}
	}

	void Reset () {
		if (transform.position.z < resetZ) {
			var newPos = transform.position + new Vector3(0f, 0f, 385f);
			transform.position = newPos;
		}
	}
}
