using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float jumpspeed = 10f;
    float xspeed = 6f;
    float xdirection;
    float xvector;
    float yspeed;
    float ydirection;
    float yvector;
    float coin_amount;

   public bool overworld;
   private bool can_jump;
   private bool is_grounded;
   private Rigidbody2D rb;

   public Transform groundCheck;
   public float groundRadius = 0.2f;
   public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        coin_amount = 0;

        xspeed = 6;
        
        yspeed = 6;

        if (overworld)
        {
            rb.gravityScale = 0f;
            can_jump = false;
        }
        else
        {
            rb.gravityScale = 2f;
            can_jump = true;
        }
    }

    private void Update()
    {
        if (can_jump = true)
        {
            Debug.Log("Jump ready!");
        }
        
        if (!overworld)
        {
            is_grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
            Debug.Log("Is Grounded: " + is_grounded);
        }
        
        xdirection = Input.GetAxis("Horizontal");
        xvector = xspeed * xdirection * Time.deltaTime;
        transform.Translate(xvector, 0, 0);
        
        if (overworld)
        {
            yspeed = 6;
        }
        else
        {
            yspeed = 0;
        }
        
        ydirection = Input.GetAxis("Vertical");
        yvector = yspeed * ydirection * Time.deltaTime;
        transform.Translate(0, yvector, 0);

        if (can_jump && is_grounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed!");
            Jump();
        }
    }

    private void Jump()
    {
        if (is_grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);
        }
    }
   
   

    //for organization, put other built-in Unity functions here





    //after all Unity functions, your own functions can go here
}