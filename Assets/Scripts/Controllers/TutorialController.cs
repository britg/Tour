using UnityEngine;
using System.Collections;

public class TutorialController : GameController {

	public GameObject panel;

	public void OnOkPress () {
		panel.SetActive(false);
	}

}
