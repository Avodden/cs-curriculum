using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    GameManager gm; 
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.coins_amount = 0;
    }
    void OnTriggerEnter2D (Collider2D other)
    // Update is called once per frame
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            gm.coins_amount += 1;
            print("you have " + gm.coins_amount + " coins");
            Destroy(other.gameObject);
        }
    }
}
