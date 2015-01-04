using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stroke : GameModel {

	public enum Pedal {
		Left,
		Right
	}

	public float speed;
	public float length;
	public Pedal pedal;

}
