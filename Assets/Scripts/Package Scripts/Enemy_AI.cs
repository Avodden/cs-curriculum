using System;
using UnityEngine;

public class Enemy_axe : MonoBehaviour
{
    private GameManager gm;
    public float chasespeed = 3f;
    public float attackdistance = 1f;
    public float patrolspeed = 2f;
    public float attackcooldown = 1f;
    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;
    private GameObject player;
    private GameObject ChaseRadius;

    private bool isChasing = false;

    private bool canAttack = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        ChaseRadius = GameObject.FindGameObjectWithTag("ChaseCollider");
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged with 'Player'.");
        }
    }

    // Update is called once per frames
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
        else
        {
            Patrol();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider triggered with: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Radius: " + gameObject.name);
            Debug.Log("Chase method will be called.");
            isChasing = true; 
            Debug.Log("isChasing set to: " + isChasing);
                 
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
        
        if (canAttack && Vector2.Distance(transform.position, player.transform.position) <= attackdistance)
        {
            Debug.Log("Player attacked!");
            gm.health_amount -= 10;
            gm.health_amount = Mathf.Clamp(gm.health_amount, 0, 100);
            Invoke(nameof(ResetAttack), attackcooldown);
        }
        
    }

    private void ResetAttack();
    {
        canAttack = true;
    }
    
    private void Patrol()
    {
        // Check if the patrol waypoints are set
        if (patrolWaypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, patrolspeed * Time.deltaTime);

        // If the enemy reaches the waypoint, switch to the next waypoint
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            // Move to the next waypoint, loop back if we're at the last waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
            Debug.Log("Moving to next waypoint: " + currentWaypointIndex);
        }
    }
}
