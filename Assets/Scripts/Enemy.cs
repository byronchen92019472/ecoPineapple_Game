using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Ship ship;
    public float speed;


	// Use this for initialization
	void Start () {
        GameObject shipObject = GameObject.FindWithTag("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime * 60, transform.position.z);
        if (transform.position.y < ship.transform.position.y - 30)
        {
            Debug.Log(ship.transform.position.y);
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            Debug.Log("Asteroid Collide with Ship");
            ship.explode();
        }
    }
}
