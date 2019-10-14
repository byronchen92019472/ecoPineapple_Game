using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class AnimatedDialog : MonoBehaviour {
    public Text textArea;
    public string[] strings1;
    public string[] strings;
    public string[] strings2, strings3;
    public float speed = 0.1f;
    public GameObject rubbishImages;
    public GameObject starportImage;

    public bool isEndScene;
    public Button continueButton;

    private ArrayList allstrings = new ArrayList(3);

    int stringIndex = 0;
    int characterIndex = 0;

    public AudioSource speechSound;

    void Start ()
    {
        strings1 = new string[10] {
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

        StartCoroutine (DisplayTimer());
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            ContinueButtonClick();
        }
    }

    IEnumerator DisplayTimer()
    {
        while (true) {
            
            yield return new WaitForSeconds(speed);
            if (characterIndex > strings [stringIndex].Length)
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

            textArea.text = strings[stringIndex].Substring(0, characterIndex);
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

        if (characterIndex < strings[stringIndex].Length)
        {
            characterIndex = strings[stringIndex].Length;
        }

        else if (stringIndex < strings.Length - 1)
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
