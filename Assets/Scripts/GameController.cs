using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject spacePort;
    public Material skyboxGround;
    public Material skyboxSpace;

    public int levelNumber = 1;
  
	// Use this for initialization
	void Start () {
        //Instantiate(ship, new Vector3(0, 0, 0), Quaternion.identity);
        //player.money = 0;
        objectList = new List<GameObject>();
        maxHeight = 0;  
        initBuildPhase();     
        resultDisplayPanel.SetActive(false);
	}
	
	void Update () {
        if (launchPhase)
        {
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
                spawnEco();
            }else if (levelNumber == 3){
                spawnEco();
                if (ship.transform.position.y > 150){
                    spawnAsteroids();
                }
            }
            if (ship.transform.position.y > 100)
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
    }

    public void initLaunchPhase()
    {
        initLevel(levelNumber);
        maxHeight = 0f;
        ship.fuel = ship.maxFuel;
        fuelSlider.maxValue = ship.fuel;
        thrustSlider.maxValue = ship.maxThrust * 2;
        distanceSlider.maxValue = moon.transform.position.y;
        enemySpawnTime = 2f;
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

    }

    void initLevel(int level){
        clearObjectList();
        if (level == 1){
            addToObjectList(spacePort, new Vector3(0, 144, 0));
            addToObjectList(spacePort, new Vector3(0.6f, 971, 0));
            addToObjectList(moon, new Vector3(-16.5f, 1000, 0));
        }
        if (level == 2){
            // addToObjectList(spacePort, new Vector3(-10, 144, 0));
            // addToObjectList(spacePort, new Vector3(-10, 971, 0));
            // addToObjectList(spacePort, new Vector3(-10, 1970, 0));
            addToObjectList(spacePort, new Vector3(0, 3470, 0));
            addToObjectList(moon, new Vector3(-16.5f, 1000, 0));
            addToObjectList(Venus, new Vector3(-5, 2000, 0));
            addToObjectList(Mercury, new Vector3(-5, 3500, 0));
        }
        if (level == 3){
            // addToObjectList(spacePort, new Vector3(-10, 144, 0));
            // addToObjectList(spacePort, new Vector3(0.6f, 971, 0));
            // addToObjectList(spacePort, new Vector3(9.4f, 2989.9f, 0));
            // addToObjectList(spacePort, new Vector3(9.4f, 4970.9f, 0));
            // addToObjectList(spacePort, new Vector3(9.4f, 6970.9f, 0));
            // addToObjectList(spacePort, new Vector3(9.4f, 8970.9f, 0));
            // addToObjectList(spacePort, new Vector3(9.4f, 10970.9f, 0));
            addToObjectList(spacePort, new Vector3(0, 14970.9f, 0));
            addToObjectList(moon, new Vector3(-16.5f, 1000, 0));
            addToObjectList(mars, new Vector3(-5.7f, 3004, 0));
            addToObjectList(Jupiter, new Vector3(-5.7f, 5000, 0));
            addToObjectList(Saturn, new Vector3(-5.7f, 7000, 0));
            addToObjectList(Neptune, new Vector3(-5.7f, 9000, 0));
            addToObjectList(Venus, new Vector3(-5.7f, 11000, 0));
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
