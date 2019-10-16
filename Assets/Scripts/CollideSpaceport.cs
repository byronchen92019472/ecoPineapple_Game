using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSpaceport : MonoBehaviour {

    public Ship ship;
    public GameController gameController;
    public string planetName;
	// Use this for initialization
	void OnEnable () {
        GameObject shipObject = GameObject.Find("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
        GameObject gameObject = GameObject.Find("GameController");
        if (gameObject != null){
            gameController = gameObject.GetComponent<GameController>();
        }
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if (other.tag == "Ship")
        {
            if (tag == "StarportEnd"){
                gameController.completeLevel();
                ship.isEndSpaceport = true;
            }
            ship.audioManager.playStarport();
            ship.dropTourists(planetName, this.transform.position);
        }
        
	}
}
