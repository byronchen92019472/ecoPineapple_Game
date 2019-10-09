using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    string scene1 = "StartScreen";
    string scene2 = "SpaceGame";
    string story1 = "Level1Story";
    string story2 = "Level2Story";
    string story3 = "Level3Story";

    private float introTime = 3.0f;

    public bool isEndScene = false;
    private bool startStart;
    private bool beginGame;

    private bool level1Load;
    private bool level2Load;
    public bool level3Load;

    // Update is called once per frame
    void Update()
    {
        introTime -= Time.deltaTime;

        // Loads Start Screen Scene after intro time expires
        if (introTime <= 0.0f && startStart == false)
        {
            SceneManager.LoadSceneAsync(scene1);
            startStart = true;
        }

        //Loads Story 1 Scene after player confirms new game
        if (level1Load == false && beginGame == true)
        {
            SceneManager.LoadScene(story1);
            SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
            beginGame = false;
            level1Load = true;

        }

        // Passes isEndScene boolean from Story Scene to this controller
        if (level1Load == true)
        {
            isEndScene = GameObject.FindGameObjectWithTag("AnimatedDialog").GetComponent<AnimatedDialog>().isEndScene;
            //spawn x
        }

        // Loads SpaceGame Scene at Level 1 location after Story 1 Scene finishes playing
        if (isEndScene = true && level1Load == true)
        {
            SceneManager.LoadScene(scene2);
        }

        //if (level2Load == false && beginGame == true)
        //{
        //    SceneManager.LoadScene(story2);
        //    beginGame = false;
        //    level2Load = true;
        //}

        //if (level1Load == false && beginGame == true)
        //{
        //    SceneManager.LoadScene(story1);
        //    beginGame = false;
        //    level3Load = true;
        //}
    }
}
