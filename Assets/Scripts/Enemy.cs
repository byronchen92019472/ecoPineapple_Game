using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Ship ship;
    public float speed;
    public GameController gamecontroller;
    public List<Sprite> asteroidImages; 

	// Use this for initialization
    void OnEnable()
    {
        GameObject shipObject = GameObject.Find("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
        int rand = Random.Range(0, 6);
        speed = Random.Range(-0.2f, 0f);
		GetComponent<SpriteRenderer>().sprite = asteroidImages[rand];
        //StartCoroutine(selfdestruct());
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
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
