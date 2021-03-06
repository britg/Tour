﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BestDistanceView : GameView {

	Player player;

	public Text text;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = string.Format ("{0}", player.bestDistance.ToString("F2"));
	}
}
