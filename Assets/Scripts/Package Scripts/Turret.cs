using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject _projectile;

    private float Fire_rate = 1.5f;

    private float Cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Cooldown < 0)
        {
            Debug.Log("Player Detected");
            GameObject clone = Instantiate(_projectile, transform.position, Quaternion.identity);
            Projectile script = clone.GetComponent<Projectile>();

            if (script != null)
            {
                script.target = other.gameObject.transform.position;
                Debug.Log("Firing at target: " + other.transform.position);
            }
            else
            {
                Debug.Log("Projectile script not found");
            }
            Cooldown = Fire_rate;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;
    }
}
