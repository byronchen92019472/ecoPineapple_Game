using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public Ship ship;
    public float offset;
	
	// Update is called once per frame
	void Update () {
        if (ship.transform.position.y > transform.position.y + 160)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (480), transform.position.z);
        }
	}
}
