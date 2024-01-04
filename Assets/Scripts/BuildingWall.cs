using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWall : AsteroidImpact
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        hp = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void DestoryObject()
    {
        Destroy(gameObject);
    }
}
