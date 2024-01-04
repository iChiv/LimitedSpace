using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AsteroidImpact : MonoBehaviour
{
    public float hp;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(1f);
        }
        Destroy(this);
    }

    void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            DestoryObject();
        }
    }

    protected abstract void DestoryObject();

}
