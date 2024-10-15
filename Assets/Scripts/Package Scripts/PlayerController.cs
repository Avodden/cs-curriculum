using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xspeed;
    float xdirection;
    float xvector;
    float yspeed;
    float ydirection;
    float yvector;
    float coin_amount;

   public bool overworld; 

    private void Start()
    {
        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        coin_amount = 0;

        xspeed = 6;
        
        yspeed = 6;

        if (overworld)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }

    private void Update()
    {
        if (overworld)
        {
            yspeed = 6;
        }
        else
        {
            yspeed = 0;
        }
        xdirection = Input.GetAxis("Horizontal");
        xvector = xspeed * xdirection * Time.deltaTime;
        transform.Translate(xvector, 0, 0);

        ydirection = Input.GetAxis("Vertical");
        yvector = yspeed * ydirection * Time.deltaTime;
        transform.Translate(0, yvector, 0);
    }
   
   

    //for organization, put other built-in Unity functions here





    //after all Unity functions, your own functions can go here
}