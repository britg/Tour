using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player : GameModel {

	public float currentSpeed;
	public float currentDistance { get; set; }
	public float bestDistance { get; set; }
	public float comfort;
	public float style;
	public int coins;
}
