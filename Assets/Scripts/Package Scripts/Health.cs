using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   GameManager gm;

   public float health;
   
   public Slider healthSlider;

   public Slider EasehealthSlider;

   private float lerpSpeed = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.health_amount = 100;
        health = gm.health_amount;
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
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
        
        if (EasehealthSlider.value != health)
        {
            EasehealthSlider.value = Mathf.Lerp(EasehealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
} 
