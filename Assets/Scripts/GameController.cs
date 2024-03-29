﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
//using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Camera frontCamera;
    public Camera buildCamera;

    public Ship ship;
    public Slider fuelSlider;
    public Slider thrustSlider;
    public Slider distanceSlider;
    public Text heightText;
    public Text maxHeightText;
    public Text velocityText;
    public Text moneyText;
    public Text touristText;
    public Text fuelWarningText;
    public Text levelText;

    public Canvas buildUI;
    public Canvas launchUI;

    public GameObject resultDisplayPanel;
    public Text resultMoney;
    public Text resultInfo;

    private float maxHeight;
    public Player player;

    public GameObject ecoProduct;
    public float ecoSpawnCounter;
    public float ecoSpawnTime;
    public int ecoHitCounter;

    public GameObject enemy;
    public Vector3 enemySpawnPosition;
    public float enemySpawnCounter;
    public float enemySpawnTime;
    public float enemyDistanceTracker;

    public bool buildPhase;
    public bool launchPhase;

    public bool deathAsteroid;

    public List<GameObject> objectList;

    public GameObject moon;
    public GameObject mars;
    public GameObject Venus;
    public GameObject Mercury;
    public GameObject Saturn;
    public GameObject Jupiter;
    public GameObject Neptune;
    public GameObject Uranus;
    public GameObject Pluto;
    public GameObject Sun;
    public GameObject spacePort;
    public GameObject spacePortEnd;
    public GameObject trash;
    public Material skyboxGround;
    public Material skyboxSpace;
    public AudioController audioController;
    public BuildButtonScript bbs;
    public MilestoneManager mm;
    public int levelNumber = 1;
  
	// Use this for initialization
	void Start () {
        //player.money = 0;
        objectList = new List<GameObject>();
        maxHeight = 0;
        LoadGame();
        initBuildPhase();     
        resultDisplayPanel.SetActive(false);
	}
	
	void Update () {
        //SaveGame();
        if (launchPhase)
        {
            enemyDistanceTracker = enemyDistanceTracker + ship.rb.velocity.y * Time.fixedDeltaTime;
            if (maxHeight < ship.transform.position.y)
            {
                maxHeight = ship.transform.position.y;
            }
            fuelSlider.value = ship.fuel;
            thrustSlider.value = ship.thrust + ship.maxThrust;
            distanceSlider.value = ship.transform.position.y;
            heightText.text = "Height: " + ((int)ship.transform.position.y).ToString();
            maxHeightText.text = "Max Height: " + ((int)maxHeight).ToString();
            if (levelNumber == 1){
                
            }else if(levelNumber == 2){
                if(ship.transform.position.y > 5){
                    spawnEco();
                }    
                if (ship.transform.position.y > 200 && ship.alive){
                    spawnAsteroids();
                }
            }else if (levelNumber >= 3){
                if(ship.transform.position.y > 5){
                    spawnEco();
                }
                if (ship.transform.position.y > 200 && ship.alive){
                    spawnAsteroids();
                }
            }
            if (ship.transform.position.y > 130)
            {
                ship.rb.useGravity = false;
                ship.thrust = 0;
            }
            if (ship.fuel < 1)
            {
                fuelWarningText.gameObject.SetActive(true);
            }
        }     
	}

    private Save CreateSaveGameObject(){
        Save save = new Save();
        save.money = player.money;
        save.maxFuel = ship.maxFuel;
        save.level = levelNumber;
        save.fuelPart1 = bbs.fuelPart1.interactable;
        save.fuelPart2 = bbs.fuelPart2.interactable;
        save.fuelPart3 = bbs.fuelPart3.interactable;
        save.fuelPart4 = bbs.fuelPart4.interactable;
        save.fuelPart5 = bbs.fuelPart5.interactable;
        save.fuelPart6 = bbs.fuelPart6.interactable;
        save.fuelPart7 = bbs.fuelPart7.interactable;
        save.fuelPart8 = bbs.fuelPart8.interactable;
        save.fuelPart9 = bbs.fuelPart9.interactable;
        save.maxTourist = bbs.maxTourist;
        save.milestoneOne = mm.milestoneOne;
        save.milestoneTwo = mm.milestoneTwo;
        save.milestoneMoon = mm.milestoneMoon;
        save.milestoneMars = mm.milestoneMars;
        save.milestoneMercury = mm.milestoneMercury;
        save.milestoneVenus = mm.milestoneVenus;
        save.milestoneJupiter = mm.milestoneJupiter;
        save.milestoneSaturn = mm.milestoneSaturn;
        save.milestoneNeptune = mm.milestoneNeptune;
        save.milestoneUranus = mm.milestoneUranus;
        save.milestonePluto = mm.milestonePluto;
        save.milestoneDisplay = mm.milestoneDisplay.text;
        save.milestoneCodes = mm.milestoneCodes.text;
        return save;
    }

    public void SaveGame(){
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame(){
        if(File.Exists(Application.persistentDataPath + "/gamesave.save")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            player.money = save.money;
            ship.maxFuel = save.maxFuel;
            levelNumber = save.level;
            mm.milestoneOne = save.milestoneOne;
            mm.milestoneTwo = save.milestoneTwo;
            mm.milestoneMoon = save.milestoneMoon;
            mm.milestoneMars = save.milestoneMars;
            mm.milestoneMercury = save.milestoneMercury;
            mm.milestoneVenus = save.milestoneVenus;
            mm.milestoneJupiter = save.milestoneJupiter;
            mm.milestoneSaturn = save.milestoneSaturn;
            mm.milestoneNeptune = save.milestoneNeptune;
            mm.milestoneUranus = save.milestoneUranus;
            mm.milestonePluto = save.milestonePluto;
            mm.milestoneDisplay.text = save.milestoneDisplay;
            mm.milestoneCodes.text = save.milestoneCodes;
        }else{
            Debug.Log("No Save File");
        }
    }

    IEnumerator showMessage(string message, Text text, int time = 5){
        text.enabled = true;
        text.text = message;    
        yield return new WaitForSeconds(time);
        text.enabled = false;
    }

    void spawnEco()
    {
        ecoSpawnCounter -= Time.deltaTime;
        if (ecoSpawnCounter < 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-enemySpawnPosition.x, enemySpawnPosition.x), ship.transform.position.y + 50, enemySpawnPosition.z);
            GameObject ecoProductObj = ObjectPooler.sharedInstance.GetPooledObject("EcoProduct");
            if (ecoProductObj != null)
            {
                ecoProductObj.transform.position = spawnPos;
                ecoProductObj.SetActive(true);
            }
            ecoSpawnCounter = ecoSpawnTime;
        }
    }

    void spawnAsteroids()
    {
        
        if(enemyDistanceTracker > 500 && enemySpawnTime > 0.3){
            //Debug.Log(enemyDistanceTracker);
            enemySpawnTime = enemySpawnTime - 0.1f;
            enemyDistanceTracker = 0f;
        }
        enemySpawnCounter -= Time.deltaTime;
        if (enemySpawnCounter < 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-enemySpawnPosition.x, enemySpawnPosition.x), ship.transform.position.y + 50, enemySpawnPosition.z);
            GameObject asteroid = ObjectPooler.sharedInstance.GetPooledObject("Enemy");
            if (asteroid != null)
            {
                asteroid.transform.position = spawnPos;
                asteroid.SetActive(true);
            }
            //Instantiate(enemy, spawnPos, Quaternion.identity);
            enemySpawnCounter = enemySpawnTime;
        }

        if (ship.fuel < 1 && !deathAsteroid)
        {
            Vector3 spawnPos = new Vector3(ship.transform.position.x, ship.transform.position.y + 50, enemySpawnPosition.z);
            GameObject asteroid = ObjectPooler.sharedInstance.GetPooledObject("Enemy");
            if (asteroid != null)
            {
                asteroid.transform.position = spawnPos;
                asteroid.SetActive(true);
            }
            deathAsteroid = true;
        }

    }

    public void launchResults()
    {
        resultDisplayPanel.SetActive(true);
        int touristMoney = (int)(ship.droppedTourists * ship.droppedHeight);
        int moneyReceived = (int)maxHeight + touristMoney;
        resultMoney.text = "You received $" + moneyReceived;
        if (touristMoney > 0)
        {
            resultInfo.text = string.Format("Max Height Reached: {0} = ${4}\n{1} Tourists dropped off at Height {2} = ${3}",
                                        (int)maxHeight, ship.droppedTourists, ship.droppedHeight, touristMoney, (int)maxHeight);
        }
        else
        {
            resultInfo.text = string.Format("Max Height Reached: {0} = ${1}",
                                        (int)maxHeight, (int)maxHeight);
        }
        
        ship.tourists = 0;
        player.money += moneyReceived;
    }

    public void initBuildPhase()
    {
        
        launchResults();
        SaveGame();
        moneyText.text = "x " + player.money.ToString();
        touristText.text = "Tourists: " + ship.tourists.ToString();
        buildUI.enabled = true;
        launchUI.enabled = false;
        ship.canLaunch = false;
        launchPhase = false;
        buildPhase = true;
        levelText.enabled = false;
        ship.resetShip();
        showBuildCamera();
        RenderSettings.skybox = skyboxGround;
        ObjectPooler.sharedInstance.ClearPooledList();
        initLevel(levelNumber);   
        audioController.playBuildMusic();
    }

    public void initLaunchPhase()
    {
        initLevel(levelNumber);
        audioController.playLaunchMusic();
        maxHeight = 0f;
        ship.fuel = ship.maxFuel;
        enemySpawnTime = 2f;
        enemyDistanceTracker = 0;
        ecoSpawnTime = 4;
        deathAsteroid = false;
        buildUI.enabled = false;
        launchUI.enabled = true;
        ship.canLaunch = true;
        buildPhase = false;
        launchPhase = true;
        levelText.enabled = false;
        showFrontCamera();
        fuelWarningText.gameObject.SetActive(false);
        if (levelNumber == 1)
            StartCoroutine(showMessage("Level 1\nFly out of Earth", levelText, 2));
        if (levelNumber == 2)
            StartCoroutine(showMessage("Level 2\nFly to Mercury", levelText, 2));
        if (levelNumber == 3)
            StartCoroutine(showMessage("Level 3\nFly to Pluto", levelText, 2));
    }

    void showFrontCamera()
    {
        frontCamera.enabled = true;
        buildCamera.enabled = false;
    }

    void showBuildCamera()
    {
        frontCamera.enabled = false;
        buildCamera.enabled = true;
    }

    public void completeLevel(){
        //string s = "Level " + levelNumber + "\nComplete";
        levelNumber++; 
        //.StartCoroutine(showMessage(s, levelText, 2));
    }

    public void playStoryScene(){
        SaveGame();
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene(){
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level1Story");
    }

    void initLevel(int level){
        clearObjectList();
        if (level == 1){
            addToObjectList(spacePortEnd, new Vector3(0, 160, 0));
            addToObjectList(trash, new Vector3(0, 40));
            addToObjectList(trash, new Vector3(-2, 40));
            addToObjectList(trash, new Vector3(2, 40));
            addToObjectList(trash, new Vector3(-8, 70));
            addToObjectList(trash, new Vector3(-10, 70));
            addToObjectList(trash, new Vector3(-12, 70));
            //addToObjectList(spacePort, new Vector3(0.6f, 971, 0));
            //addToObjectList(moon, new Vector3(-16.5f, 1000, 0));
        }
        if (level == 2){
            // addToObjectList(spacePort, new Vector3(-10, 144, 0));
            // addToObjectList(spacePort, new Vector3(-10, 971, 0));
            // addToObjectList(spacePort, new Vector3(-10, 1970, 0));
            addToObjectList(spacePortEnd, new Vector3(0, 2170, 0));
            addToObjectList(moon, new Vector3(-16.5f, 700, 0));
            addToObjectList(Venus, new Vector3(-5, 1500, 0));
            addToObjectList(Mercury, new Vector3(-5, 2200, 0));
            addToObjectList(Sun, new Vector3(-5, 3000, 0));
        }
        if (level == 3){
            addToObjectList(spacePort, new Vector3(-10, 160, 0));
            addToObjectList(spacePort, new Vector3(-10, 971, 0));
            addToObjectList(spacePort, new Vector3(-10, 2989.9f, 0));
            addToObjectList(spacePort, new Vector3(-10, 4970.9f, 0));
            addToObjectList(spacePort, new Vector3(-10, 6970.9f, 0));
            addToObjectList(spacePort, new Vector3(-10, 8970.9f, 0));
            addToObjectList(spacePort, new Vector3(-10, 10970.9f, 0));
            addToObjectList(spacePortEnd, new Vector3(0, 14970.9f, 0));
            addToObjectList(moon, new Vector3(-16.5f, 1000, 0));
            addToObjectList(mars, new Vector3(-5.7f, 3004, 0));
            addToObjectList(Jupiter, new Vector3(-5.7f, 5000, 0));
            addToObjectList(Saturn, new Vector3(-5.7f, 7000, 0));
            addToObjectList(Uranus, new Vector3(-5.7f, 9000, 0));
            addToObjectList(Neptune, new Vector3(-5.7f, 11000, 0));
            addToObjectList(Pluto, new Vector3(-5.7f, 15000, 0));
        }

    }

    void addToObjectList(GameObject obj, Vector3 pos){
        objectList.Add((GameObject)Instantiate(obj, pos, Quaternion.identity));
    }

    void clearObjectList(){
        foreach(GameObject obj in objectList){
            Destroy(obj);
        }
    }
}
