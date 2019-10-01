using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public Ship ship;
    public float offset = -160;
    public float moveSpeed = 1;
	
	// Update is called once per frame
	void Update () {
        //if (ship.transform.position.y > transform.position.y + 160)
        //{
         //   transform.position = new Vector3(transform.position.x, transform.position.y + (480), transform.position.z);
        //}
        if (ship.rb.velocity.y != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.fixedDeltaTime * 60, transform.position.z);
            if (transform.position.y < offset)
            {
                transform.position = new Vector3(transform.position.x, 480, transform.position.z);
            }
        }
        
	}
}
