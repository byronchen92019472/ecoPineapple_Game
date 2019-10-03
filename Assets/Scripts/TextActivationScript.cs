﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class TextActivationScript : MonoBehaviour
{

    public Canvas exitCanvas; //Your target for the refference
    public Canvas settingsCanvas;
    public Canvas newgameCanvas;
    public Canvas continueCanvas;
    private BoxCollider2D bc;
    private bool atExit;
    private bool atSettings;
    private bool atStart;
    private bool atContinue;
    private bool atClose;


    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (atStart == true && (CrossPlatformInputManager.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") > 0))  //(Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            SceneManager.LoadScene("SpaceGame");
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
        atExit = false;
        atSettings = false;
        atStart = false;
        atContinue = false;
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