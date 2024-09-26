using System;
using UnityEngine;

public class Health : MonoBehaviour
{
   GameManager gm;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            gm.health_amount -= 10;
            gm.health_amount = Mathf.Clamp(gm.health_amount, 0, 100);
            print("you have " + gm.health_amount + " health left"); 
        }
    }

    void Update()
    {
        
    }
} 
