using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneManager : MonoBehaviour {

    public List<string> milestoneList = new List<string>();
    public Text milestoneText;
    public string milestoneString;
    public static MilestoneManager instance = null;

    //void Awake()
    //{
    //    //Check if instance already exists
    //    if (instance == null)

    //        //if not, set instance to this
    //        instance = this;

    //    //If instance already exists and it's not this:
    //    else if (instance != this)

    //        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
    //        Destroy(gameObject);

    //    //Sets this to not be destroyed when reloading scene
    //    DontDestroyOnLoad(this.gameObject);
    //}

    // Use this for initialization
    void Start () {
        milestoneList.Add("1. Reach 500m. [Code: 123 - $5 Off any order over $20]");
        milestoneList.Add("2. Reach Earth Starport.");
    }

    // Update is called once per frame
    void Update () {
        updateUIText();
    }

    public void updateUIText()
    {
        milestoneString = "";

        foreach (string value in milestoneList)
        {
            milestoneString = milestoneString + value + "\n";
        }

        milestoneText.text = milestoneString;
        //GameObject.FindGameObjectWithTag("MilestoneText").GetComponent<UnityEngine.UI.Text>().text = milestoneString;
    }

    //public void updateMilestoneList()
    //{
    //    milestoneList = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().milestoneList;
    //}
}
