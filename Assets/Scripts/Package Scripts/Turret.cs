using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject _projectile;

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
            Debug.LogWarning("Player Detected");
            GameObject clone = Instantiate(_projectile, transform.position, Quaternion.identity);
            Projectile script = clone.GetComponent<Projectile>();

            if (script != null)
            {
                script.target = other.gameObject.transform.position;
                Debug.LogWarning("Firing at target: " + other.transform.position);
            }
            else
            {
                Debug.LogWarning("Projectile script not found");
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
