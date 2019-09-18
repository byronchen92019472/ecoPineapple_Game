﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchButtonScript : MonoBehaviour {

    public Button endButton;

    public Text fuelDisplay;
    public Text thrustDisplay;

    public GameController gameController;
    private Ship ship;
    
    void OnEnable()
    {
        endButton.onClick.AddListener(() => endButtonClick());
        ship = gameController.ship;
    }

    void Update()
    {
        fuelDisplay.text = "Fuel: " + ((int) ship.fuel).ToString() + " / " + ship.maxFuel.ToString();
        thrustDisplay.text = "Thrust: " + ship.thrust.ToString() + " / " + ship.maxThrust.ToString();
    }

    void endButtonClick()
    {
        gameController.initBuildPhase();
    }
}
