using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Rigidbody rb; 
    public ParticleSystem rocketThrust;
    public GameObject explosion;
    public ShipParts shipParts;
    public GameObject rocketSprite;
    private ParticleSystem[] childParticles;


    private float velocityBeforeCollision;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rocketThrust.Stop();
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
            
            //transform.position = new Vector3(transform.position.x, transform.position.y + thrust * Time.deltaTime * 60, transform.position.z);
        }

        if (fuel <= 0 || thrust == 0)
        {
            thrust = 0;
            rocketThrust.Stop();
        }

        if (thrust > 0)
        {
            fuel -= (1 * thrust / maxThrust) * fuelEfficiencyMultiplier * Time.fixedDeltaTime * 60;
            rocketThrust.Play();
        }
        else if (thrust < 0)
        {
            fuel -= (1 * -thrust / maxThrust) * fuelEfficiencyMultiplier * Time.fixedDeltaTime * 60;
            rocketThrust.Play();
        }
        if (canLaunch)
        {
            handleMovement();
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
        stopExplode();
    }

    public void explode()
    {
        alive = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        explosion.SetActive(true);
        rocketSprite.SetActive(false);
    }

    void stopExplode()
    {
        alive = true;
        explosion.SetActive(false);
        rocketSprite.SetActive(true);
    }

    void handleMovement()
    {
        if (fuel > 0)
        {
            if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                if (thrust < maxThrust)
                {
                    thrust += 1 * Time.fixedDeltaTime * 60;
                }
            }
            else if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                if (thrust > -maxThrust)
                {
                    thrust -= 1 * Time.fixedDeltaTime * 60;
                }
            }
            if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - (turnSpeed * Time.fixedDeltaTime * 60), transform.position.y, 0);
                fuel -= 1 * 0.5f * Time.fixedDeltaTime * 60;
            }
            else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + (turnSpeed * Time.fixedDeltaTime * 60), transform.position.y, 0);
                fuel -= 1 * 0.5f * Time.fixedDeltaTime * 60;
            }
        }

    }

    public void dropTourists(string name, float height)
    {
        droppedTourists = tourists;
        droppedHeight = height;
    }

}
