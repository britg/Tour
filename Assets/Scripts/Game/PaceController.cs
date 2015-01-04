using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PaceController : GameController {

	Player player;
	bool hasStarted = false;
	public GameObject strokeContainer;
	public RectTransform leftPedal;
	public RectTransform rightPedal;
	public GameObject leftStrokePrefab;
	public GameObject rightStrokePrefab;

	public float strokeSpeed;
	public float strokeCadence {
		get { return player.cadence; }
	}

	List<GameObject> strokeObjs;


	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		player = GetPlayer();
		player.Load();
		strokeObjs = new List<GameObject>();
		GetZone();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.fallen) {
			DetectInput();
			NextStroke();
			CalcDistance();
			ApplyDrag();
			DetectFallen();
		}
	}

	float currentStrokeTime = 0f;
	Stroke.Pedal lastPedal = Stroke.Pedal.Right;
	void NextStroke () {
		currentStrokeTime += Time.deltaTime;
		if (currentStrokeTime >= strokeCadence) {
			currentStrokeTime = 0f;
			if (lastPedal == Stroke.Pedal.Right) {
				CreateLeftStroke();
			} else {
				CreateRightStroke();
			}
		}
	}

	void CreateLeftStroke () {
		GameObject strokeObj = (GameObject)Instantiate(leftStrokePrefab);
		InitStroke(strokeObj, Stroke.Pedal.Left);
	}

	void CreateRightStroke () {
		GameObject strokeObj = (GameObject)Instantiate(rightStrokePrefab);
		InitStroke(strokeObj, Stroke.Pedal.Right);
	}

	void InitStroke (GameObject strokeObj, Stroke.Pedal pedal) {
		strokeObj.transform.SetParent(strokeContainer.transform, false);
		Stroke stroke = GetStroke(strokeObj);
		stroke.speed = strokeSpeed;
		stroke.pedal = pedal;
		strokeObjs.Add(strokeObj);
		lastPedal = pedal;
	}

	Stroke GetStroke (GameObject strokeObj) {
		return strokeObj.GetComponent<StrokeController>().stroke;
	}

	float missY  = -10f;
	void DetectMisses () {
		List<GameObject> toCull = new List<GameObject>();
		foreach (GameObject strokeObj in strokeObjs) {
			if (strokeObj.transform.position.y < missY) {
				toCull.Add(strokeObj);
			}
		}

		foreach (GameObject cullStroke in toCull) {
			CullStroke(cullStroke);
		}
	}

	void CullStroke (GameObject strokeObj) {
		strokeObjs.Remove(strokeObj);
		Destroy(strokeObj);
	}

	void DetectInput () {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			OnLeftPedalPress();
		}

		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			OnRightPedalPress();
		}
	}

	public void OnLeftPedalPress () {
		OnPedalPress(Stroke.Pedal.Left);
	}

	public void OnRightPedalPress () {
		OnPedalPress(Stroke.Pedal.Right);
	}

	float zoneTop;
	float zoneBottom;
	float halfHeight;
	float center;
	Rect leftRect;
	Rect rightRect;
	void GetZone () {
		halfHeight = leftPedal.rect.height/2f;
		center  = leftPedal.position.y;
		zoneTop =  center + halfHeight;
		zoneBottom = center - halfHeight;
		leftRect = leftPedal.rect;
		rightRect = rightPedal.rect;
	}

	bool inZone (float y) {
		return (y <= zoneTop && y > zoneBottom);
	}

	bool inZone (GameObject strokeObj) {
		float strokeHalfHeight = strokeObj.GetComponent<RectTransform>().rect.height/2f;
		bool top = strokeObj.transform.position.y <= (center + halfHeight + strokeHalfHeight);
		bool bot = strokeObj.transform.position.y >= (center - halfHeight - strokeHalfHeight);
		return top && bot;
	}

	float accuracy (float y) {
		return 1f - Mathf.Abs(y - center) / leftRect.height;
	}

	void OnPedalPress (Stroke.Pedal pedal) {
		hasStarted = true;
		foreach (GameObject strokeObj in strokeObjs) {
			float hit = strokeObj.transform.position.y;
			if (inZone(strokeObj)) {
				Stroke stroke = GetStroke(strokeObj);
				if (stroke.pedal == pedal) {
					float acc = accuracy(hit);
					player.Boost(acc);
					CullStroke (strokeObj);
					return;
				}
			}
		}

		// did not hit in zone
		player.PedalPenalty();
	}

	void CalcDistance () {
		player.currentDistance += player.currentSpeed/3600f * Time.deltaTime;
	}

	void ApplyDrag () {
		if (!hasStarted) {
			return;
		}
		player.ChangeSpeed(-Time.deltaTime*player.drag);
	}

	void DetectFallen () {
		if (player.currentSpeed < 1f) {
			player.fallen = true;
			player.Save();
		}
	}
}
