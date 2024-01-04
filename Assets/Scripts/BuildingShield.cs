using System;
using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            hp -= 1;
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
