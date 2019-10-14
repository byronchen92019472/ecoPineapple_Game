using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecoproduct : MonoBehaviour {

    public Ship ship;
    public int speed = 0;

    // Use this for initialization

    void OnEnable()
    {
        GameObject shipObject = GameObject.Find("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
        //StartCoroutine(selfdestruct());
    }


    // Update is called once per frame
    void Update()
    {
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
            ship.player.money += 10;
            ship.audioManager.playPickup();
            gameObject.SetActive(false);
        }
    }

    IEnumerator selfdestruct()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
