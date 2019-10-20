using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AnimatedDialog : MonoBehaviour {
    public Text textArea;
    //public string[] strings;
    //public string[] strings2, strings3;
    public float speed = 0.03f;
    public GameObject rubbishImages;
    public GameObject starportImage;
    public GameObject ecoProductImage;
    public GameObject touristImage;
    public Image elon;
    public Sprite elon_Sprite1;
    public Sprite elon_Sprite2;
    public Sprite elon_Sprite3;

    public string[] readString;
    public int levelNumber;

    public bool isEndScene;
    public Button continueButton;

    int stringIndex = 0;
    int characterIndex = 0;

    public AudioSource speechSound;

    void Start ()
    {
        LoadGame();
        if (levelNumber == 1){
            elon.sprite = elon_Sprite1;
            readString  = new string[11] {
                "Hello, my name is Elon and I am an investor for this space tourism company",
                "I have been tasked to guide you in constructing a suitable spacecraft for space tourism",
                "Your first test is to upgrade the spacecraft that is able to reach our planetary starport",
                "Be careful though. You want to avoid the space trash layer orbitting the Earth",
                "Purchase fuel upgrades after every launch to increased distance reached",
                "Navigate the rocket using the left and right thrusters to avoid the following rubbish obstacles...",
                "",
                "Once you are close enough, the starport's tractor beam will guide you in for docking.\n\nThe starport looks like this...",
                "",
                "\u0022When something is important enough, you do it even if the odds are not in your favor\u0022",
                "Good Luck!"
            };
        }
        if (levelNumber == 2){
            elon.sprite = elon_Sprite2;
            readString = new string[11] {
                "Good job with successfully docking at the spaceport",
                "\u0022If you get up in the morning and think the future is going to be better, it is a bright day\u0022",
                "Today will be a bright day for you as you trial our future solution",
                "For your next mission, you will deliver rubbish to the spaceport of Mercury",
                "We need to test if it is viable to throw our rubbish into the Sun",
                "Just be careful to still avoid garbage and any asteroids along the way",
                "Also, please collect any \u0022rubbish\u0022 that seems reusable",
                "These are deemed eco-products and we will provide bonus money for each one you collect",
                "These eco-products will look like this...",
                "",
                "It is going to be fun throwing our problems into the fiery inferno. Let's go!"
            };
        }
        if (levelNumber == 3){
            elon.sprite = elon_Sprite3;
            readString  = new string[14] {
                "Yeah...um...it is currently not economically feasible to throw rubbish into the sun.", 
                "Oh well. Can't say I didn't try", 
                "Currently, Earth is not going to be habitable in the foreseeable future", 
                "People will eventually be migrating to collosal starports orbitting each planet", 
                "They want to experience what it is like to live on a starport before making their big commitment to move there", 
                "And so, for this mission we need to deliver tourists to our planetary starports scattered across our solor system", 
                "Make sure to add the amount of tourists you want to deliver to a starport. They ain't picky on which one they arrive at", 
                "To add tourists, navigating to the tourist screen via the tab in the build screen. You can also upgrade the passenger bay size to hold more tourists", 
                "The tourist screen looks like this...", 
                "",
                "Any successful tourist delivery will result in a money bonus for you to spend", 
                "Please do not let the rocket explode with tourists onboard. Our profits will decrease as we pay for life damages", 
                "This is going to be a long mission.You will need unrelenting focus to achieve this task. Stay strong!", 
                "We're going to make it happen. As God is my bloody witness, I'm hell-bent on making it work."
            };
        }
        if (levelNumber == 4){
            elon.sprite = elon_Sprite1;
            readString  = new string[1] {
                "Congrats you win"
            };
        }
        //Debug.Log(readString[0]);

        StartCoroutine (DisplayTimer());
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            ContinueButtonClick();
        }
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

    IEnumerator DisplayTimer()
    {
        while (true) {
            yield return new WaitForSeconds(speed);
            if (characterIndex > readString [stringIndex].Length)
            {
                continue;
            }
            if (stringIndex == 6 && levelNumber == 1)
            {
                rubbishImages.SetActive(true);
            }
            else
            {
                rubbishImages.SetActive(false);
            }
            if (stringIndex == 8 && levelNumber == 1)
            {
                starportImage.SetActive(true);
            }
            else
            {
                starportImage.SetActive(false);
            }
            if (stringIndex == 9 && levelNumber == 2)
            {
                ecoProductImage.SetActive(true);
            }
            else
            {
                ecoProductImage.SetActive(false);
            }
            if (stringIndex == 9 && levelNumber == 3)
            {
                touristImage.SetActive(true);
            }
            else
            {
                touristImage.SetActive(false);
            }


            textArea.text = readString[stringIndex].Substring(0, characterIndex);
            characterIndex++;
        }
    }

    private void OnEnable()
    {
        continueButton.onClick.AddListener(() => ContinueButtonClick());
    }

    void ContinueButtonClick()
    {
        speechSound.Play(0);

        if (characterIndex < readString[stringIndex].Length)
        {
            characterIndex = readString[stringIndex].Length;
        }

        else if (stringIndex < readString.Length - 1)
        {
            stringIndex++;
            characterIndex = 0;
        }
        else
        {
            isEndScene = true;
            SceneManager.LoadSceneAsync("SpaceGame");
        }
    }

    // Update is called once per frame
 //   void Update () {
 //       if (continueButton.
            
 //           //.GetKeyDown(KeyCode.Space))
 //       {
 //           speechSound.Play(0);

 //           if (characterIndex < strings [stringIndex].Length)
 //           {
 //               characterIndex = strings[stringIndex].Length;
 //           }

 //           else if (stringIndex < strings.Length)
 //           {
 //               stringIndex++;
 //               characterIndex = 0;
 //           }
 //       }
	//}
}
