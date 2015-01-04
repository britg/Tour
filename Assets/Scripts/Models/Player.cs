using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player : GameModel {
	static string BEST_DISTANCE = "bestdistance";
	static string COINS = "coins";

	public float comfort;
	public float style;
	public int coins;

	public float currentDistance { get; set; }
	public float bestDistance { get; set; }

	public float currentSpeed;
	public float minSpeed = 1f;
	public float maxSpeed = 100f;

	public float maxDrag;
	public float minDrag;
	public float drag {
		get {
			return CalcDrag();
		}
	}

	public float maxCadence;
	public float minCadence;
	public float cadence {
		get { return CalcCadence(); }
	}

	public float maxBoost;
	public float minBoost;

	public float falsePedalPenalty;

	public bool fallen { get; set; }

	public void ChangeSpeed (float amount) {
		float newSpeed = currentSpeed + amount;
		SetSpeed(newSpeed);
	}

	public void SetSpeed (float newSpeed) {
		currentSpeed = Mathf.Clamp(newSpeed, minSpeed, maxSpeed);
	}

	public float speedPercent {
		get { return currentSpeed / maxSpeed; }
	}

	public float CalcDrag () {
		return speedPercent * (maxDrag - minDrag) + minDrag;
	}

	public float CalcCadence () {
		return maxCadence - speedPercent * (maxCadence - minCadence);
	}

	public void Boost (float accuracy) {
		float increase = accuracy * (maxBoost - minBoost) + minBoost;
		ChangeSpeed(increase);
	}

	public void PedalPenalty () {
		ChangeSpeed(falsePedalPenalty);
	}

	public void Load () {
		LoadBestDistance();
		LoadCoins();
	}

	void LoadBestDistance () {
		if (ES2.Exists(BEST_DISTANCE)) {
			bestDistance = ES2.Load<float>(BEST_DISTANCE);
		}
	}

	void LoadCoins () {
		if (ES2.Exists(COINS)) {
			coins = ES2.Load<int>(COINS);
		}
	}

	public void Save () {
		SaveBestDistance();
		SaveCoins();
	}

	void SaveBestDistance () {
		if (currentDistance > bestDistance) {
			ES2.Save(currentDistance, BEST_DISTANCE);
		}
	}

	void SaveCoins () {
		ES2.Save(coins, COINS);
	}
}
