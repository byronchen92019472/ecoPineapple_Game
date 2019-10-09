using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSpaceport : MonoBehaviour {

    public Ship ship;
    //public GameController gameController;
    public string planetName;
	// Use this for initialization
	void OnEnable () {
        GameObject shipObject = GameObject.Find("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if (other.tag == "Ship")
        {
            ship.dropTourists(planetName, this.transform.position.y);
            Debug.Log("Collide with Ship");
            Debug.Log(ship.rb.velocity.y);

        }
        
	}
}
