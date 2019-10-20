using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

public class TextActivationScript : MonoBehaviour
{
    public Canvas exitCanvas; //Your target for the refference
    public Canvas settingsCanvas;
    public Canvas newgameCanvas;
    public Canvas continueCanvas;
    public Canvas milestonesCanvas;
    public Canvas milestonesDisplayCanvas;
    private BoxCollider2D bc;
    private bool atExit;
    private bool atSettings;
    public bool atStart;
    private bool atContinue;
    private bool atClose;
    private bool atMilestones;
    public Animation walk2;

    public int levelNumber;
    public AudioSource footstepAudio;
    public AudioClip footnoiseClip;

    public Button left;
    public Button right;
    public Button confirm;

    private float stepCounter = 0;
    
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        //LoadGame();
        //testing
        //bool sc = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneController>().level3Load;
        //Debug.Log(sc);

        #if UNITY_STANDALONE_WIN
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            confirm.gameObject.SetActive(false);
        #endif
    }

    void Update()
    {
        if (atContinue == true && (CrossPlatformInputManager.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") > 0))  //(Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            SceneManager.LoadScene("SpaceGame");
        }

        if (atStart == true && (CrossPlatformInputManager.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") > 0))  //(Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            SaveGame();
            SceneManager.LoadScene("Level1Story");
            //SceneManager.LoadSceneAsync("SpaceGame");
        }

        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            stepCounter = stepCounter + Time.deltaTime;
            if (stepCounter > 0.15)
            {
                footstepAudio.PlayOneShot(footnoiseClip, 0.2f);
                stepCounter = 0;
            }
            
        }
        
        if (atMilestones == true && (CrossPlatformInputManager.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") > 0))  //(Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            //GameObject.FindGameObjectWithTag("SceneTraveller").GetComponent<MilestoneManager>().updateUIText();
            milestonesDisplayCanvas.gameObject.SetActive(true);
        }
    }
    
    private Save CreateSaveGameObject(){
        Save save = new Save();
        save.money = 5;
        save.maxFuel = 150;
        save.level = 1;
        save.fuelPart1 = true;
        save.fuelPart2 = true;
        save.fuelPart3 = true;
        save.fuelPart4 = true;
        save.fuelPart5 = true;
        save.fuelPart6 = true;
        save.fuelPart7 = true;
        save.fuelPart8 = true;
        save.fuelPart9 = true;
        save.milestoneOne = false;
        save.milestoneTwo = false;
        save.milestoneMoon = false;
        save.milestoneMars = false;
        save.milestoneMercury = false;
        save.milestoneVenus = false;
        save.milestoneJupiter = false;
        save.milestoneSaturn = false;
        save.milestoneNeptune = false;
        save.milestoneUranus = false;
        save.milestonePluto = false;
        save.milestoneDisplay = "";
        save.milestoneCodes = "";
        save.maxTourist = 2;
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

            levelNumber = save.level;
        }else{
            Debug.Log("No Save File");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            //myCanvas.enabled = true;
            exitCanvas.gameObject.SetActive(true);
            atExit = true;
        }

        if (other.tag == "Settings")
        {
            //myCanvas.enabled = true;
            settingsCanvas.gameObject.SetActive(true);
            atSettings = true;
            
        }

        if (other.tag == "Milestones")
        {
            //myCanvas.enabled = true;
            milestonesCanvas.gameObject.SetActive(true);
            atMilestones = true;
        }

        if (other.tag == "NewGame")
        {
            //myCanvas.enabled = true;
            newgameCanvas.gameObject.SetActive(true);
            atStart = true;
        }

        if (other.tag == "Continue")
        {
            //myCanvas.enabled = true;
            continueCanvas.gameObject.SetActive(true);
            atContinue = true;
        }

        if (other.tag == "Close")
        {
            //myCanvas.enabled = true;
            Application.Quit();
        }
    }

    void OnTriggerExit2D()
    {
        exitCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        newgameCanvas.gameObject.SetActive(false);
        continueCanvas.gameObject.SetActive(false);
        milestonesCanvas.gameObject.SetActive(false);
        milestonesDisplayCanvas.gameObject.SetActive(false);
        atExit = false;
        atSettings = false;
        atStart = false;
        atContinue = false;
        atMilestones = false;
    }

    //If you want to be more specific to what gets enabled and store it all in one script you can check tags


}

//    private BoxCollider2D bc;
//    private Canvas myCanvas;

//    // Use this for initialization
//    void Start () {
//        bc = GetComponent<BoxCollider2D>();
//        myCanvas = GetComponentInChildren<Canvas>();
//    }

//	// Update is called once per frame
//	void Update () {
//		if (bc.isTrigger)
//        {
//            myCanvas.gameObject.SetActive(true);
//        }
//        else
//        {
//            myCanvas.gameObject.SetActive(false);
//        }
//	}

//    private void OnTriggerEnter(Collider other)
//    {
//        // Using the tag method.
//        if (other.tag == "Settings")
//        {
//            other.gameObject.SetActive(true);
//            //Here the object do the jump.
//        }

//        else


//        if (other.tag == "NewGame")
//        {
//            other.gameObject.SetActive(true);
//            //Here the object do the other stuff.
//        }

//        // Using the layer method, you need to make a reference to the index not the name.
//        if (other.tag == "Continue")
//        {
//            other.gameObject.SetActive(true);
//            //Here the object do the jump.
//        }

//        if (other.tag == "Exit")
//        {
//            other.gameObject.SetActive(true);
//            //Here the object do the other stuff.
//        }

//        else
//    }
//}
