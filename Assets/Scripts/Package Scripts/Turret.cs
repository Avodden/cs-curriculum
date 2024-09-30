using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject target;
    private GameObject _projectile;

    private float Fire_rate = 2;

    private float Cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Cooldown < 0)
        {
           GameObject clone = Instantiate(_projectile); 
           GetComponent<Projectile>();
            
            Cooldown = Fire_rate;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;
    }
}
