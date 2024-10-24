using System;
using UnityEngine;

public class Enemy_axe : MonoBehaviour
{
    public float chasespeed = 2f;
    public float attackdistance = 1f;

    private GameObject player;
    private GameObject ChaseRadius;

    private bool isChasing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChaseRadius = GameObject.FindGameObjectWithTag("ChaseCollider");
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged with 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isChasing && player != null)
        {
            ChasePlayer();
            // Check if in attack range
            if (Vector2.Distance(transform.position, player.transform.position) <= attackdistance)
            {      
                AttackPlayer();
            }
            
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider triggered with: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Radius: " + gameObject.name);
            if ( gameObject == ChaseRadius)
            {
                Debug.Log("Chase method will be called.");
                isChasing = true;
                Debug.Log("isChasing set to: " + isChasing);
            }     
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            Debug.Log("Player left the chase area");
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing Player");
        
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chasespeed * Time.deltaTime);
        Debug.Log("Chasing Player to position: " + player.transform.position);
        
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking Player");
        
        if (Vector2.Distance(transform.position, player.transform.position) <= attackdistance)
        {
            Debug.Log("Player attacked!");
        }

    }
}
