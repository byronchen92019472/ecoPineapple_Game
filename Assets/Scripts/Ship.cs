using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public float thrust;
    public float maxThrust;
    public float turnSpeed = 1;
    public float fuel;
    public float fuelEfficiencyMultiplier;

    public Rigidbody rb; 
    public ParticleSystem rocketThrust;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rocketThrust.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(transform.up * thrust);
        if (fuel <= 0)
        {
            thrust = 0;
            rocketThrust.Stop();
        }

        if (thrust > 0)
        {
            fuel -= 1;
            rocketThrust.Play();
        }

        handleMovement();    
	}

    void handleMovement()
    {
        if (fuel > 0)
        {
            if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                if (thrust < maxThrust)
                {
                    thrust += 1;
                }
            }
            else if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                if (thrust > 0)
                {
                    thrust -= 1;
                }
            }
        }

        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * turnSpeed, Space.World);
        }
        else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnSpeed, Space.World);
        }
    }
}
