﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSpaceport : MonoBehaviour {

    public Ship ship;
    public GameController gameController;
    public string planetName;
	// Use this for initialization
	void Start () {
        GameObject shipObject = GameObject.FindWithTag("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        ship.dropTourists(planetName, this.transform.position.y);
        Debug.Log("Collide with Ship");
        Debug.Log(ship.rb.velocity.y);
	}
}