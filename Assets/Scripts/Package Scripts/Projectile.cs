using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameManager gm;
    public Vector3 target;
    public float Projectile_speed = 20f;
    private float Deathtime = 0.1f;
    public float lifetime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * (Projectile_speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.health_amount -= 10;
            Destroy(gameObject, Deathtime);
        }
    }
}
