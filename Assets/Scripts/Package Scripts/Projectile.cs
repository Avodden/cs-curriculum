using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameManager gm;
    public Vector3 target;
    private Vector3 direction;
    public float Projectile_speed;
    public float Deathtime;
    public float lifetime;
    public int Projectile_damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = (target - transform.position).normalized;
        Destroy(gameObject, lifetime);
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * (Projectile_speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gm != null)
            {
                gm.health_amount -= Projectile_damage;
                Debug.Log("Player hit: " + gm.health_amount);
            }
            else
            {
                Debug.Log("Gm not found");
            }
            Destroy(gameObject, Deathtime);
        }
    }
}
