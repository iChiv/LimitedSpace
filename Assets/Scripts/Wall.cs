using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float bounceForce = 10f;

    public float bounceSpaceship = 30f;

    void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();


        if (rb != null)
        {
            Vector2 normal = other.contacts[0].normal;
            Vector2 bounceDirection = -normal.normalized;
            if (other.gameObject.CompareTag("Player"))
            {
                rb.AddForce(bounceDirection * bounceForce * bounceSpaceship);
            }
            else
            {
                rb.AddForce(bounceDirection * bounceForce);
            }
        }
    }
}
