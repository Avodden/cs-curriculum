using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 target;
    public float Projectile_speed = 20f;

    public float lifetime = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
