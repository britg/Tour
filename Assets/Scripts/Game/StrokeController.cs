using UnityEngine;
using System.Collections;

public class StrokeController : GameController {

	public Stroke stroke;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var newPos = transform.position;
		newPos.y -= stroke.speed * Time.deltaTime;
		transform.position = newPos;
	}
}
