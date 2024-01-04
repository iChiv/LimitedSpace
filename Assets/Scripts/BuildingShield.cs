using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShield : AsteroidImpact
{
    public float health;
    public SpaceShip spaceShip;

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

    protected override void DestoryObject()
    {
        Destroy(gameObject);
    }
}
