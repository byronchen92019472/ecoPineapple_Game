using System.Collections;
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
    public Text touristText;
    public Text fuelWarningText;

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
    public bool milestoneOne;
    public bool milestoneTwo;
    public bool milestoneThree;
    public bool milestoneFour;

    public bool deathAsteroid;

    public GameObject moon;
    public Material skyboxGround;
    public Material skyboxSpace;

    public Text milestoneText;
    public List<string> milestoneList;
  
	// Use this for initialization
	void Start () {
        //Instantiate(ship, new Vector3(0, 0, 0), Quaternion.identity);
        ship.maxFuel = 150;
        ship.fuel = ship.maxFuel;
        ship.maxThrust = 25;
        ship.fuelEfficiencyMultiplier = .95f;
        player.money = 0;

        maxHeight = 0;
        initBuildPhase();
        resultDisplayPanel.SetActive(false);
        milestoneText.enabled = false;
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
            spawnEco();
            if (ship.transform.position.y > 1 && !milestoneOne)
            {
                StartCoroutine(showMilestone("Milestone Reached\nFirst Flight"));
                milestoneOne = true;
            }
            if (ship.transform.position.y > 100 && !milestoneTwo)
            {
                StartCoroutine(showMilestone("Milestone Reached\nOut of the Earths Atmosphere"));
                milestoneTwo = true;
            }
            if (ship.transform.position.y > 100)
            {
                //RenderSettings.skybox = skyboxSpace;
                spawnAsteroids();
                ship.rb.useGravity = false;
                ship.thrust = 0;
            }
            if (ship.transform.position.y > 1000 && !milestoneThree)
            {
                StartCoroutine(showMilestone("Milestone Reached\nReached the Moon"));
                milestoneThree = true;
            }
            if (ship.transform.position.y > 3000 && !milestoneFour)
            {
                StartCoroutine(showMilestone("Milestone Reached\nReached Mars"));
                milestoneThree = true;
            }

            if (ship.fuel < 1)
            {
                fuelWarningText.gameObject.SetActive(true);
            }

        }
        
	}

    IEnumerator showMilestone(string message)
    {
        milestoneText.enabled = true;
        milestoneText.text = message;    
        yield return new WaitForSeconds(5);
        milestoneText.enabled = false;
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

        //milestoneList = GameObject.FindGameObjectWithTag("SceneTraveller").GetComponent<MilestoneManager>().milestoneList;
        //milestoneList.Add("3. Reach Moon Starport. [Code: 5LC - Free Delivery]");
        //GameObject.FindGameObjectWithTag("SceneTraveller").GetComponent<MilestoneManager>().updateMilestoneList();
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
        ship.resetShip();
        showBuildCamera();
        RenderSettings.skybox = skyboxGround;
    }

    public void initLaunchPhase()
    {
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
        showFrontCamera();
        fuelWarningText.gameObject.SetActive(false);
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
