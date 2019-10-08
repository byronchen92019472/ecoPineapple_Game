using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Ship ship;
    public float speed;
    public GameController gamecontroller;

	// Use this for initialization
    void OnEnable()
    {
        GameObject shipObject = GameObject.Find("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
        StartCoroutine(selfdestruct());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime * 60, transform.position.z);
        if (transform.position.y < ship.transform.position.y - 30)
        {
            gameObject.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            Debug.Log("Asteroid Collide with Ship");
            gameObject.SetActive(false);
            ship.explode();
        }
    }

    IEnumerator selfdestruct()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
