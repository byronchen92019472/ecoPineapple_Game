using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController2 : MonoBehaviour {

    private float speed = 10; //20 WINDOWS
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    //public GameObject animation;
    public Animator anim;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0); WINDOWS
        Vector2 moveInput = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), 0);
        moveVelocity = moveInput * speed;
        if (moveInput[0] != 0)
        {
            //gameObject.SetActive(true);
            anim.SetBool("isWalking", true);

        }
        else
        {
            anim.SetBool("isWalking", false);
        }
	}

    private void FixedUpdate()
    {
        if (moveVelocity[0] != 0)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
            //anim.SetBool("isWalking", true);
        }
        if (moveVelocity[0] == 0)
        {
            //anim.SetBool("isWalking", false);
        }

        if (moveVelocity[0] > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveVelocity[0] < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
