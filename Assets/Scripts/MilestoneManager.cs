using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneManager : MonoBehaviour {
    public bool milestoneOne;
    public bool milestoneTwo;
    public bool milestoneMoon;
    public bool milestoneMars;
    public bool milestoneMercury;
    public bool milestoneVenus;
    public bool milestoneJupiter;
    public bool milestoneSaturn;
    public bool milestoneNeptune;
    public bool milestoneUranus;
    public bool milestonePluto;

    public Ship ship;
    public Text milestoneText;
    public Text milestoneDisplay;
    public GameController gameController;
    public string milestoneDisplayString;
    public Text milestoneCodes;

    void Start(){
        milestoneText.enabled = false;
        GameObject gameObject = GameObject.Find("GameController");
        if (gameObject != null){
            gameController = gameObject.GetComponent<GameController>();
        }
    }

    void OnEnable() {
        milestoneDisplay.text = milestoneDisplayString;
    }

    void Update(){
        if (ship.transform.position.y > 3 && !milestoneOne){
            StartCoroutine(showMilestone("Milestone Reached\nFirst Flight"));
            //milestoneDisplayString += "First Flight\n";
            milestoneDisplay.text += "First Flight\n";
            milestoneOne = true;
        }
        if (ship.transform.position.y > 140 && !milestoneTwo){
            StartCoroutine(showMilestone("Milestone Reached\nOut of the Earths Atmosphere"));
            milestoneDisplay.text +="Out of the Earths Atmosphere\n";
            milestoneTwo = true;
        }
        if (ship.transform.position.y > 700 && !milestoneMoon){
            StartCoroutine(showMilestone("Milestone Reached\nReached the Moon"));
            milestoneDisplay.text += "Reached the Moon\n";
            milestoneMoon = true;
        }
        if (ship.transform.position.y > 1500 && !milestoneVenus && gameController.levelNumber == 2){
            StartCoroutine(showMilestone("Milestone Reached\nReached Venus"));
            milestoneDisplay.text += "Reached Venus\n";
            milestoneVenus = true;
        }
        if (ship.transform.position.y > 2200 && !milestoneMercury && gameController.levelNumber == 2){
            StartCoroutine(showMilestone("Milestone Reached\nReached Mercury"));
            milestoneDisplay.text += "Reached Mercury\n";
            milestoneMercury = true;
        }
        if (ship.transform.position.y > 2970 && !milestoneMars && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Mars"));
            milestoneDisplay.text += "Reached Mars\n";
            milestoneMars = true;
        }
        if (ship.transform.position.y > 4970 && !milestoneJupiter && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Jupiter"));
            milestoneDisplay.text += "Reached Jupiter\n";
            milestoneJupiter = true;
        }
        if (ship.transform.position.y > 6970 && !milestoneSaturn && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Saturn"));
            milestoneDisplay.text += "Reached Saturn\n";
            milestoneSaturn = true;
        }
        if (ship.transform.position.y > 8970 && !milestoneNeptune && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Neptune"));
            milestoneDisplay.text += "Reached Neptune\n";
            milestoneNeptune = true;
        }
        if (ship.transform.position.y > 10970 && !milestoneUranus && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Uranus"));
            milestoneDisplay.text += "Reached Uranus\n";
            milestoneUranus = true;
        }
        if (ship.transform.position.y > 14970 && !milestonePluto && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Pluto"));
            milestoneDisplay.text += "Reached Pluto\n";
            milestonePluto = true;
        }
    }

    IEnumerator showMilestone(string message){
        milestoneText.enabled = true;
        milestoneText.text = message;    
        yield return new WaitForSeconds(5); 
        milestoneText.enabled = false;
    }
}
