using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public Ship ship;
    public float offset = -160;
    public Vector3 move;
    public float moveSpeed = 1;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, ship.transform.position.y, transform.position.z);
	}
}
