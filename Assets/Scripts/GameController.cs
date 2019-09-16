using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera frontCamera;
    public Camera highCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            showFrontCamera();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            showHighCamera();
        }
	}

    void showFrontCamera()
    {
        frontCamera.enabled = true;
        highCamera.enabled = false;
    }

    void showHighCamera()
    {
        frontCamera.enabled = false;
        highCamera.enabled = true;
    }
}
