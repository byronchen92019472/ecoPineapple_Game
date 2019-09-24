﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

    public float thrust;
    public float maxThrust;
    public float turnSpeed = 1;
    public float fuel;
    public float maxFuel;
    public float fuelEfficiencyMultiplier = 1;

    public bool canLaunch;

    public Rigidbody rb; 
    public ParticleSystem rocketThrust;
    public GameObject explosion;
    public ShipParts shipParts;
    private ParticleSystem[] childParticles;


    private float velocityBeforeCollision;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rocketThrust.Stop();
        canLaunch = false;
	}

    void FixedUpdate()
    {
        velocityBeforeCollision = rb.velocity.y;
        rb.AddForce(new Vector3(0, 1 * Time.fixedDeltaTime * 60, 0) * thrust);

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

    void OnCollisionEnter()
    {
        if (velocityBeforeCollision< -30)
        {
            Debug.Log("Collision");
            explode();
        }
        
    }

    public void resetShip()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.position = new Vector3(0f, 0f, 0f);
        rb.rotation = Quaternion.identity;
        thrust = 0;
        stopExplode();
    }

    void explode()
    {
        explosion.SetActive(true);
        childParticles = explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in childParticles)
        {
            p.Play();
        }
    }

    void stopExplode()
    {
        childParticles = explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in childParticles)
        {
            p.Stop();
        }
        Debug.Log("Stop Explode");
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