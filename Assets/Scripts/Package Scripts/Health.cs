using UnityEngine;

public class Health : MonoBehaviour
{
   GameManager gm;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.health_amount = 3;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            gm.health_amount -= 1;
            print("you have " + gm.health_amount + " health left"); 
        }
    }
}
