using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    public GameObject ImpactEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ImpactEffect != null)
        {
            Instantiate(ImpactEffect, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }
}
