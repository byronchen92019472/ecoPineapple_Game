using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Ship : MonoBehaviour {

    public float thrust;
    public float maxThrust;
    public float turnSpeed = 0.2f;
    public float fuel;
    public float maxFuel;
    public float fuelEfficiencyMultiplier = 1;
    public int tourists;
    public int maxTourists;
    public int droppedTourists;
    public float droppedHeight;

    public bool alive;
    public bool canLaunch;
    public bool launchUp;
    public bool canMoveToSpaceport;
    private Vector3 spaceportPos;

    public GameObject flames;
    public Rigidbody rb; 
    public GameObject explosion;
    public ShipParts shipParts;
    public GameObject rocketSprite;
    public GameObject launchUpButton;


    private float velocityBeforeCollision;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        maxFuel = 150;
        fuel = maxFuel;
        maxThrust = 25;
        fuelEfficiencyMultiplier = .95f;
        stopExplode();
        canLaunch = false;
        alive = true;
	}

    void FixedUpdate()
    {
        velocityBeforeCollision = rb.velocity.y;
        if (alive)
        {
            if (rb.velocity.y <= 35)
            {
                rb.AddForce(new Vector3(0, 1 * Time.fixedDeltaTime * 60, 0) * thrust);
            }
        }

        if (fuel <= 0 || thrust == 0)
        {
            thrust = 0;
            flames.SetActive(false);
        }

        if (thrust > 0)
        {
            fuel -= (1 * thrust / maxThrust) * fuelEfficiencyMultiplier * Time.fixedDeltaTime * 60;
            flames.SetActive(true);
        }
        if (canLaunch && alive)
        {
            handleMovement();
        }
        if(launchUp)
            launch();
        if (canMoveToSpaceport){
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, spaceportPos, 0.1f * Time.fixedDeltaTime * 60);
            if(Vector3.Distance(transform.position, spaceportPos) < 0.1f){
                rb.velocity = Vector3.zero;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (velocityBeforeCollision< -10)
        {
            explode();     
        }    
    }

    public void resetShip()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.position = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
        rb.rotation = Quaternion.identity;
        thrust = 0;
        launchUp = false;
        canMoveToSpaceport = false;
        stopExplode();
        launchUpButton.SetActive(true);
        flames.SetActive(false);
    }

    public void explode()
    {
        alive = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        explosion.SetActive(true);
        rocketSprite.SetActive(false);
        flames.SetActive(false);
    }

    void stopExplode()
    {
        alive = true;
        explosion.SetActive(false);
        rocketSprite.SetActive(true);
    }

    void launch(){
        if (thrust < maxThrust)
        {
            thrust += 1 * Time.fixedDeltaTime * 60;
        }
    }

    void handleMovement()
    {
        if (fuel > 0)
        {
            if (CrossPlatformInputManager.GetAxis("Vertical") > 0)//(Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                launchUp = true;
                launchUpButton.SetActive(false);
            }
            if (CrossPlatformInputManager.GetAxis("Horizontal") < 0) // (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - (turnSpeed * Time.fixedDeltaTime * 60), transform.position.y, 0);
                fuel -= 1 * Time.fixedDeltaTime * 60;
            }
            else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)  //(Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + (turnSpeed * Time.fixedDeltaTime * 60), transform.position.y, 0);
                fuel -= 1 * Time.fixedDeltaTime * 60;
            }
        }

    }

    public void dropTourists(string name, Vector3 pos)
    {
        canMoveToSpaceport = true;
        spaceportPos = pos;
        alive = false;
        droppedTourists = tourists;
        droppedHeight = pos.y;
    }

}
