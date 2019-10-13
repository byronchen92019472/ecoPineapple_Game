using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AnimatedDialog : MonoBehaviour {
    public Text textArea;
    public string[] strings;
    public float speed = 0.1f;

    public bool isEndScene;

    public Button continueButton;

    int stringIndex = 0;
    int characterIndex = 0;

    public AudioSource speechSound;

    void Start ()
    {
        StartCoroutine (DisplayTimer());
    }

    IEnumerator DisplayTimer()
    {
        while (true) {
            yield return new WaitForSeconds(speed);
            if (characterIndex > strings [stringIndex].Length)
            {
                continue;
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
