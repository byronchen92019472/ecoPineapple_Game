using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchButtonScript : MonoBehaviour {

    public Button endButton;

    public Text fuelDisplay;
    public Text thrustDisplay;

    public Button left;
    public Button right;
    public Button up;

    public GameController gameController;
    private Ship ship;
    
    void OnEnable()
    {
        endButton.onClick.AddListener(() => endButtonClick());
        ship = gameController.ship;

        #if UNITY_STANDALONE_WIN
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            up.gameObject.SetActive(false);
        #endif
    }

    void Update()
    {
        fuelDisplay.text = "Fuel: " + ((int) ship.fuel).ToString() + " I " + ship.maxFuel.ToString();
        thrustDisplay.text = "Thrust: " + ((int)ship.thrust).ToString() + " I " + ship.maxThrust.ToString();
    }

    void endButtonClick()
    {
        gameController.initBuildPhase();
    }
}
