using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Ship ship;
    public int speed = 0;
	public List<Sprite> trashImages; 

    void OnEnable()
    {
        GameObject shipObject = GameObject.Find("Ship");
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
		int rand = Random.Range(0, 3);
		GetComponent<SpriteRenderer>().sprite = trashImages[rand];
    }

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
