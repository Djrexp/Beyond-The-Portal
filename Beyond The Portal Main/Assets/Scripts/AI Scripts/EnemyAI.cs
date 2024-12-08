using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    private Transform currentTarget;

    
    public float patrolSpeed = 2f;
    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointA; // Start patrolling to pointA
    }

    private void Update(){
        PatrolBehavior();
    }

    // Handles how an AI patrols the level
    private void PatrolBehavior(){
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * patrolSpeed, direction.y * patrolSpeed);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f){
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

}
