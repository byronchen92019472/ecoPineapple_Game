using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonScript : MonoBehaviour {

    public Button launchButton;

    public GameController gameController;


	// Use this for initialization
	void OnEnable () {
        launchButton.onClick.AddListener(() => launchButtonClick());
	}

    void launchButtonClick()
    {
        gameController.initLaunchPhase();
    }
}
