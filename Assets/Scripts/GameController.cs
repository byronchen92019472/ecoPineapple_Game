﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Camera frontCamera;
    public Camera highCamera;
    public Camera firstPersonCamera;

    public Ship ship;
    public Slider fuelSlider;
    public Slider thrustSlider;
    public Text heightText;
    public Text maxHeightText;
    public Text velocityText;

    public Canvas buildUI;
    public Canvas launchUI;

    private float maxHeight;
    public Player player;

    public bool buildPhase;
    public bool launchPhase;

	// Use this for initialization
	void Start () {
        ship.maxFuel = 500;
        ship.fuel = ship.maxFuel;
        ship.maxThrust = 40;
        ship.fuelEfficiencyMultiplier = .95f;

        maxHeight = 0;
        initBuildPhase();

        showFrontCamera();
	}
	
	// Update is called once per frame
	void Update () {
        fuelSlider.value = ship.fuel;
        thrustSlider.value = ship.thrust;
        heightText.text = "Height: " + ship.transform.position.y.ToString();
        maxHeightText.text = "Max Height: " + maxHeight.ToString();
        velocityText.text = "Velocity: " + ship.rb.velocity.y.ToString();

        if (maxHeight < ship.transform.position.y)
        {
            maxHeight = ship.transform.position.y;
        }

        if (launchPhase)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                showFrontCamera();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                showHighCamera();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                showFirstPersonCamera();
            }
        }
        
	}

    public void initBuildPhase()
    {
        buildUI.enabled = true;
        launchUI.enabled = false;
        ship.canLaunch = false;
        launchPhase = false;
        buildPhase = true;
        ship.resetShip();
    }

    public void initLaunchPhase()
    {
        ship.fuel = ship.maxFuel;
        fuelSlider.maxValue = ship.fuel;
        thrustSlider.maxValue = ship.maxThrust;

        buildUI.enabled = false;
        launchUI.enabled = true;
        ship.canLaunch = true;
        buildPhase = false;
        launchPhase = true;
    }

    void showFrontCamera()
    {
        frontCamera.enabled = true;
        highCamera.enabled = false;
        firstPersonCamera.enabled = false;
    }

    void showHighCamera()
    {
        frontCamera.enabled = false;
        highCamera.enabled = true;
        firstPersonCamera.enabled = false;
    }

    void showFirstPersonCamera()
    {
        frontCamera.enabled = false;
        highCamera.enabled = false;
        firstPersonCamera.enabled = true;
    }
}
