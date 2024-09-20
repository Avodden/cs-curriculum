using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   GameManager gm;

   public Slider healthSlider;

   public Slider easehealthSlider;

   private float lerpSpeed = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.health_amount = 100;
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            gm.health_amount -= 10;
            print("you have " + gm.health_amount + " health left"); 
        }
    }

    void Update()
    {
        if (healthSlider.value != gm.health_amount)
        {
            healthSlider.value = gm.health_amount;
        }

        if (healthSlider.value != easehealthSlider.value)
        {
            easehealthSlider.value = MathF.SmoothStep(easehealthSlider.value, gm.health_amount, lerpSpeed);
        }
    }
} 
