using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneManager : MonoBehaviour {

    public List<string> milestoneList = new List<string>();
    public Text milestoneText;
    private string milestoneString;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(milestoneText);
    }

    // Use this for initialization
    void Start () {
        milestoneList.Add("1. Reach 500m. [Code: 123 - $5 Off any order over $20]");
        milestoneList.Add("2. Reach Earth Starport.");

        updateUIText();
    }
	
	// Update is called once per frame
	void Update () {
    }

    void updateUIText()
    {
        milestoneString = "";

        foreach (string value in milestoneList)
        {
            milestoneString = milestoneString + value + "\n";
        }

        milestoneText.text = milestoneString;
        //milestoneText.
    }
}
