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

    private Rigidbody2D rb;

    private BoxCollider2D boxCollider;

    public LayerMask wallLayer;

    private float stuckThreshold = 0.2f; 
    private Vector2 lastPosition;  

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        ChaseRadius = GameObject.FindGameObjectWithTag("ChaseCollider");
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged with 'Player'.");
        }

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (rb == null || boxCollider == null)
        {
            Debug.Log("Enemy is missing rb or boxCollider");
        }

        SetClosestWaypoint(); 
        lastPosition = transform.position;
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            ChasePlayer();
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
        Vector2 nextPosition = (Vector2)transform.position + direction * chasespeed * Time.deltaTime;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f, wallLayer);

        if (hit.collider != null)
        {
            Debug.Log("Wall detected! Enemy cannot move.");
            return; 
        }
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chasespeed * Time.deltaTime);
        Debug.Log("Chasing Player to position: " + player.transform.position);
    }

    private void AttackPlayer()
    {
        if (canAttack && Vector2.Distance(transform.position, player.transform.position) <= attackdistance)
        {
            Debug.Log("Attacking Player");
            gm.health_amount -= 10;
            gm.health_amount = Mathf.Clamp(gm.health_amount, 0, 100);
            
            canAttack = false;
            Invoke(nameof(ResetAttack), attackcooldown);
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    private void Patrol()
    {
        if (patrolWaypoints.Length == 0) return;

        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, wallLayer);
        
        if (hit.collider != null)
        {
            Debug.Log("Obstacle detected while patrolling! Enemy cannot move.");
            
            if (Vector2.Distance(lastPosition, transform.position) < stuckThreshold)
            {
                Debug.Log("Enemy is stuck, recalculating closest waypoint.");
                SetClosestWaypoint();
            }
            return; 
        }
        
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, patrolspeed * Time.deltaTime);

        lastPosition = transform.position;
        
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            NextWaypoint();
        }
    }

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length; // Move to the next waypoint, wrap around
        Debug.Log("Moving to next waypoint: " + currentWaypointIndex);
    }
    private void SetClosestWaypoint()
    {
        if (patrolWaypoints.Length == 0) return;

        float closestDistance = Mathf.Infinity;
        int closestIndex = 0;
        
        for (int i = 0; i < patrolWaypoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, patrolWaypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        
        currentWaypointIndex = closestIndex;
        Debug.Log("Patrolling to closest waypoint: " + currentWaypointIndex);
    }
}


