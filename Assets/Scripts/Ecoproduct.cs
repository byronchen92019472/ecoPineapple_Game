using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecoproduct : MonoBehaviour {

    public Ship ship;
    public GameController gamecontroller;
    public float speed;

    // Use this for initialization
    void Start()
    {
        GameObject shipObject = GameObject.FindWithTag("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
        GameObject gameControlObject = GameObject.FindWithTag("GameController");
        if (gameControlObject != null)
        {
            gamecontroller = gameControlObject.GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime * 60, transform.position.z);
        if (transform.position.y < ship.transform.position.y - 30)
        {
            //Debug.Log(ship.transform.position.y);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            gamecontroller.player.money += 5;
            Debug.Log("Asteroid Collide with Ship");
            Destroy(gameObject);
        }
    }
}
