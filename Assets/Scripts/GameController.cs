﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Camera frontCamera;
    public Camera highCamera;
    public Camera firstPersonCamera;
    public Camera buildCamera;

    public Ship ship;
    public Slider fuelSlider;
    public Slider thrustSlider;
    public Slider distanceSlider;
    public Text heightText;
    public Text maxHeightText;
    public Text velocityText;
    public Text moneyText;

    public Canvas buildUI;
    public Canvas launchUI;

    private float maxHeight;
    public Player player;

    public bool buildPhase;
    public bool launchPhase;

    public GameObject moon;

	// Use this for initialization
	void Start () {
        ship.maxFuel = 150;
        ship.fuel = ship.maxFuel;
        ship.maxThrust = 25;
        ship.fuelEfficiencyMultiplier = .95f;
        player.money = 0;

        maxHeight = 0;
        initBuildPhase();

        
	}
	
	// Update is called once per frame
	void Update () {
        fuelSlider.value = ship.fuel;
        thrustSlider.value = ship.thrust + ship.maxThrust;
        distanceSlider.value = ship.transform.position.y;
        heightText.text = "Height: " + ((int)ship.transform.position.y).ToString();
        maxHeightText.text = "Max Height: " + ((int)maxHeight).ToString();
        velocityText.text = "Velocity: " + ((int)ship.rb.velocity.y).ToString();

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

    public void launchResults()
    {
        player.money += (int)maxHeight * 100;
    }

    public void initBuildPhase()
    {
        launchResults();
        moneyText.text = "Money: " + player.money.ToString();
        buildUI.enabled = true;
        launchUI.enabled = false;
        ship.canLaunch = false;
        launchPhase = false;
        buildPhase = true;
        ship.resetShip();
        showBuildCamera();
    }

    public void initLaunchPhase()
    {
        maxHeight = 0f;
        ship.fuel = ship.maxFuel;
        fuelSlider.maxValue = ship.fuel;
        thrustSlider.maxValue = ship.maxThrust * 2;
        distanceSlider.maxValue = moon.transform.position.y;

        buildUI.enabled = false;
        launchUI.enabled = true;
        ship.canLaunch = true;
        buildPhase = false;
        launchPhase = true;
        showFrontCamera();
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

    void showBuildCamera()
    {
        frontCamera.enabled = false;
        highCamera.enabled = false;
        firstPersonCamera.enabled = false;
        buildCamera.enabled = true;
    }
}
