﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject ship;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - ship.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector3(transform.position.x, ship.transform.position.y + offset.y, transform.position.z) ;
	}
}
