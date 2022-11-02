using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isFacingLeft = true;
    private Rigidbody2D rb;
    private Animator animator;

    public float waypointLeft;
    public float waypointRight;

    public float speed;
    public float chaseSpeed;
    public float health;
    public float rayLenght;

    public GameObject deathEffect;
    public Transform upperRaycastOriginPoint;
    public Transform lowerRaycastOriginPoint;
    public LayerMask playerLayer;
    public PlayerStats playerStats;

    private Transform target;
    private bool targetInAgroRange = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFacingLeft && !Agro())
        {
            if(transform.position.x <= waypointLeft)
            {
                isFacingLeft = false;
                transform.Rotate(0f, 180f, 0f);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else if(!isFacingLeft && !Agro())
        {
            if(transform.position.x >= waypointRight)
            {
                isFacingLeft = true;
                transform.Rotate(0f, 180f, 0f);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }

        if (Agro())
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
        
    }

    private bool Agro()
    {
        Vector2 dir;
        if (isFacingLeft)
        {
            dir = -Vector2.right;
        }
        else
        {
            dir = Vector2.right;
        }
        
        RaycastHit2D upperRaycastHit = Physics2D.Raycast(upperRaycastOriginPoint.position, dir, rayLenght, playerLayer);
        RaycastHit2D lowerRaycastHit = Physics2D.Raycast(lowerRaycastOriginPoint.position, dir, rayLenght, playerLayer);
        Color rayColor = Color.red;

        if(upperRaycastHit.collider != null || lowerRaycastHit.collider != null)
        {
            targetInAgroRange = true;
            if(upperRaycastHit.collider != null)
            {
                target = upperRaycastHit.collider.transform;
            }else if(lowerRaycastHit.collider != null)
            {
                target = lowerRaycastHit.collider.transform;
            }
            
            rayColor = Color.green;
        }
        else
        {
            targetInAgroRange = false;
            target = null;
            rayColor = Color.red;
        }
        Debug.DrawRay(upperRaycastOriginPoint.position, dir * rayLenght, rayColor);
        Debug.DrawRay(lowerRaycastOriginPoint.position, dir * rayLenght, rayColor);
        return targetInAgroRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            
            TakeDamage(1);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.TakeDamage(1);
        }
    }

    private void TakeDamage(float amount)
    {
        health = health - amount;

        if(health <= 0)
        {
            Die();
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("Chasing", true);
        if(transform.position.x < target.position.x)
        {
            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-chaseSpeed, rb.velocity.y);
        }
    }

    private void StopChasingPlayer()
    {
        animator.SetBool("Chasing", false);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
