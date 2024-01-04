using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingSpinner : AsteroidImpact
{
    public float rotationSpeed = 30f;
    public float health = 3f;
    public float bounceForce = 5f;

    private void Start()
    {
        hp = health;
    }

    private void Update()
    {
        transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
    }

    protected override void DestoryObject()
    {
        Destroy(gameObject);
    }

    private new void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Asteroid"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = other.transform.position - transform.position;
                rb.AddForce(forceDirection.normalized * bounceForce, ForceMode2D.Impulse);
            }
        }
        
        base.OnCollisionEnter2D(other);
    }
}
