using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Asteroid : AsteroidImpact
{
    public Rigidbody2D rb;

    [Range(0,50)]
    public float TorqueMax = 1.0f;

    [Range(0, 5)]
    public float SpeedMax = 5.0f;

    //public float speedMin = 1.0f; 
    //public float speedMax = 5.0f;
    //private float speed;

    public Sprite[] asteroidSprites;

    private Vector2 movementDirection;
    public Vector3 centerAreaPoint;
    private AsteroidGenerater asteroidGenerater;

    private bool hasAppearedOnScreen = false;

    void Start()
    {
        hp = 1f;
        
        asteroidGenerater = GameObject.FindObjectOfType<AsteroidGenerater>();

        GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        transform.position = asteroidGenerater.GetRandomSpawnPositionOutsideCameraView();
        
        Vector3 centerAreaPoint = GetRandomPointInScreenCenterArea();
        movementDirection = (centerAreaPoint - transform.position).normalized;
        
        rb.velocity = movementDirection * SpeedMax;
        rb.angularVelocity = Random.Range(-TorqueMax, TorqueMax);

    }

    Vector3 GetRandomPointInScreenCenterArea()
    {
        float minX = 0.2f; 
        float maxX = 0.2f;
        float minY = 0.2f; 
        float maxY = 0.2f;

        
        Vector3 minScreenPoint = Camera.main.ViewportToWorldPoint(new Vector3(minX, minY, 0));
        Vector3 maxScreenPoint = Camera.main.ViewportToWorldPoint(new Vector3(maxX, maxY, 0));
        
        float randomX = Random.Range(minScreenPoint.x, maxScreenPoint.x);
        float randomY = Random.Range(minScreenPoint.y, maxScreenPoint.y);

        return new Vector3(randomX, randomY, 0);
    }

    private new void OnCollisionEnter2D(Collision2D other)
    {
        TakeDamage(1f);
    }

    private void Update()
    {
        if (IsVisibleFromCamera(Camera.main))
        {
            hasAppearedOnScreen = true;
        }
        else if (hasAppearedOnScreen)
        {
            Destroy(gameObject);
        }
    }

    bool IsVisibleFromCamera(Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider2D>().bounds);
    }

    protected override void DestoryObject()
    {
        base.DestoryObject();
        Destroy(gameObject);
    }


}
