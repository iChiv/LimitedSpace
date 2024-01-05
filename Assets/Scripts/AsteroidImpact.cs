using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AsteroidImpact : MonoBehaviour
{
    public float hp;
    public GameObject destructionVFX;
    public AudioClip destructionSound;
    public float volume = 1f; 

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(1f);
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            DestoryObject();
        }
    }

    protected virtual void DestoryObject()
    {
        // 播放音效
        if (destructionSound != null)
        {
            AudioSource.PlayClipAtPoint(destructionSound, transform.position, volume);
        }

        // 生成特效
        if (destructionVFX != null)
        {
            Instantiate(destructionVFX, transform.position, Quaternion.identity);
        }
    }

}
