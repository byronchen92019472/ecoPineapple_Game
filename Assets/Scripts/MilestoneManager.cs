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
    public GameController gameController;

    void Start(){
        milestoneText.enabled = false;
        GameObject gameObject = GameObject.Find("GameController");
        if (gameObject != null){
            gameController = gameObject.GetComponent<GameController>();
        }
    }

    void Update(){
        if (ship.transform.position.y > 3 && !milestoneOne){
            StartCoroutine(showMilestone("Milestone Reached\nFirst Flight"));
            milestoneOne = true;
        }
        if (ship.transform.position.y > 100 && !milestoneTwo){
            StartCoroutine(showMilestone("Milestone Reached\nOut of the Earths Atmosphere"));
            milestoneTwo = true;
        }
        if (ship.transform.position.y > 1000 && !milestoneMoon){
            StartCoroutine(showMilestone("Milestone Reached\nReached the Moon"));
            milestoneMoon = true;
        }
        if (ship.transform.position.y > 2000 && !milestoneVenus && gameController.levelNumber == 2){
            StartCoroutine(showMilestone("Milestone Reached\nReached the Venus"));
            milestoneMoon = true;
        }
        if (ship.transform.position.y > 3500 && !milestoneMercury && gameController.levelNumber == 2){
            StartCoroutine(showMilestone("Milestone Reached\nReached the Mercury"));
            milestoneMoon = true;
        }
        if (ship.transform.position.y > 3000 && !milestoneMars && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Mars"));
            milestoneMars = true;
        }
        if (ship.transform.position.y > 5000 && !milestoneJupiter && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Jupiter"));
            milestoneJupiter = true;
        }
        if (ship.transform.position.y > 7000 && !milestoneSaturn && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Saturn"));
            milestoneSaturn = true;
        }
        if (ship.transform.position.y > 9000 && !milestoneNeptune && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Neptune"));
            milestoneNeptune = true;
        }
        if (ship.transform.position.y > 11000 && !milestoneVenus && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Venus"));
            milestoneVenus = true;
        }
        if (ship.transform.position.y > 15000 && !milestonePluto && gameController.levelNumber == 3){
            StartCoroutine(showMilestone("Milestone Reached\nReached Pluto"));
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
