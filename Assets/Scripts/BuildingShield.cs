using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingShield : AsteroidImpact
{
    public float health;
    public SpaceShip spaceShip;
    public BuildingShield[] existingShields;
    public AudioClip shieldBroken;

    private void Start()
    {
        spaceShip = GameObject.FindObjectOfType<SpaceShip>();
        hp = health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            hp -= 1f;
            if (destructionSound != null)
            {
                AudioSource.PlayClipAtPoint(destructionSound, other.transform.position, volume);
            }

            // 生成特效
            if (destructionVFX != null)
            {
                Instantiate(destructionVFX, other.transform.position, Quaternion.identity);
            }
        }

        if (hp <= 0f)
        {
            DestoryObject();
        }
    }

    //not working
    public void AdjustShieldSize()
    {
        existingShields = spaceShip.GetComponentsInChildren<BuildingShield>(true);
        Debug.Log(existingShields.Length);
    
        float maxScale = 1f;
    
        foreach (var shield in existingShields)
        {
            if (shield != this && shield.transform.localScale.x > maxScale)
            {
                maxScale = shield.transform.localScale.x;
            }
        }
        
        transform.localScale =  new Vector3(maxScale + 0.1f, maxScale + 0.1f, maxScale + 0.1f);
    }

    protected override void DestoryObject()
    {
        if (shieldBroken != null)
        {
            AudioSource.PlayClipAtPoint(shieldBroken, transform.position, volume);
        }
        Destroy(gameObject);
    }
}
