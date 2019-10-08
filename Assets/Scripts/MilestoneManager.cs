using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneManager : MonoBehaviour {
    public bool milestoneOne;
    public bool milestoneTwo;
    public bool milestoneThree;
    public bool milestoneFour;

    public Ship ship;
    public Text milestoneText;

    void Start(){
        milestoneText.enabled = false;
    }

    void Update(){
        if (ship.transform.position.y > 1 && !milestoneOne){
            StartCoroutine(showMilestone("Milestone Reached\nFirst Flight"));
            milestoneOne = true;
        }
        if (ship.transform.position.y > 100 && !milestoneTwo){
            StartCoroutine(showMilestone("Milestone Reached\nOut of the Earths Atmosphere"));
            milestoneTwo = true;
        }
        if (ship.transform.position.y > 1000 && !milestoneThree){
            StartCoroutine(showMilestone("Milestone Reached\nReached the Moon"));
            milestoneThree = true;
        }
        if (ship.transform.position.y > 3000 && !milestoneFour){
            StartCoroutine(showMilestone("Milestone Reached\nReached Mars"));
            milestoneThree = true;
        }
    }

    IEnumerator showMilestone(string message){
        milestoneText.enabled = true;
        milestoneText.text = message;    
        yield return new WaitForSeconds(5);
        milestoneText.enabled = false;
    }
}
