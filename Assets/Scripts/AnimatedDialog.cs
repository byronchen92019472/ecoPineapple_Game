using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class AnimatedDialog : MonoBehaviour {
    public Text textArea;
    //public string[] strings;
    //public string[] strings2, strings3;
    public float speed = 0.1f;
    public GameObject rubbishImages;
    public GameObject starportImage;
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
            readString  = new string[10] {
                "Hello, my name is Elon and I am an investor for this space tourism company",
                "I have been tasked to guide you in constructing a suitable spacecraft for space tourism",
                "Your first test is to build a spacecraft that is able to reach our planetary starport",
                "Be careful though. You want to avoid the space trash layer orbitting the Earth",
                "Navigate the rocket using the left and right thrusters to avoid the following rubbish obstacles...",
                "",
                "Once you are close enough, the starport's tractor beam will guide you in for docking.\n\nThe starport looks like this...",
                "",
                "\u0022When something is important enough, you do it even if the odds are not in your favor\u0022",
                "Good Luck!"
            };
        }
        if (levelNumber == 2){
            readString  = new string[10] {
                "asdadsadHello, my name is Elon and I am an investor for this space tourism company",
                "I have been tasked to guide you in constructing a suitable spacecraft for space tourism",
                "Your first test is to build a spacecraft that is able to reach our planetary starport",
                "Be careful though. You want to avoid the space trash layer orbitting the Earth",
                "Navigate the rocket using the left and right thrusters to avoid the following rubbish obstacles...",
                "",
                "Once you are close enough, the starport's tractor beam will guide you in for docking.\n\nThe starport looks like this...",
                "",
                "\u0022When something is important enough, you do it even if the odds are not in your favor\u0022",
                "Good Luck!"
            };
        }
        if (levelNumber == 3){
            readString  = new string[10] {
                "Hello, my name is Elon and I am an investor for this space tourism company",
                "I have been tasked to guide you in constructing a suitable spacecraft for space tourism",
                "Your first test is to build a spacecraft that is able to reach our planetary starport",
                "Be careful though. You want to avoid the space trash layer orbitting the Earth",
                "Navigate the rocket using the left and right thrusters to avoid the following rubbish obstacles...",
                "",
                "Once you are close enough, the starport's tractor beam will guide you in for docking.\n\nThe starport looks like this...",
                "",
                "\u0022When something is important enough, you do it even if the odds are not in your favor\u0022",
                "Good Luck!"
            };
        }
        Debug.Log(readString[0]);

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

            if (stringIndex == 5)
            {
                rubbishImages.SetActive(true);
            }

            else
            {
                rubbishImages.SetActive(false);
            }

            if (stringIndex == 7)
            {
                starportImage.SetActive(true);
            }

            else
            {
                starportImage.SetActive(false);
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
