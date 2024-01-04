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
        }

        if (hp <= 0f)
        {
            DestoryObject();
        }
    }

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
        Destroy(gameObject);
    }
}
