using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidGenerater : MonoBehaviour
{
    public GameObject asteroidPrefab;

    [Range(0.01f,5)]
    public float spawnInterval = 1f;
    
    public float minimumSpawnInterval = 1f;
    public float decreaseRate = 0.1f;

    private float _nextSpawnTime;


    void Start()
    {
        // InvokeRepeating("SpawnAsteroid", spawnInterval, spawnInterval);
        _nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnAsteroid();
            _nextSpawnTime = Time.time + spawnInterval;
            if (spawnInterval > minimumSpawnInterval)
            {
                spawnInterval -= decreaseRate * Time.deltaTime;
                spawnInterval = Mathf.Max(spawnInterval, minimumSpawnInterval);
            }
        }
    }


    void SpawnAsteroid()
    {
        Vector3 spawnPosition = GetRandomSpawnPositionOutsideCameraView();
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }

    public Vector3 GetRandomSpawnPositionOutsideCameraView()
    {
        Camera cam = Camera.main;
        Vector2 screenPosition = Vector2.zero;

        switch (Random.Range(0, 4))
        {
            case 0: 
                screenPosition = new Vector2(Random.value, 1.1f);
                break;
            case 1: 
                screenPosition = new Vector2(Random.value, -0.1f);
                break;
            case 2: 
                screenPosition = new Vector2(-0.1f, Random.value);
                break;
            case 3: 
                screenPosition = new Vector2(1.1f, Random.value);
                break;
        }

        Vector3 worldPosition = cam.ViewportToWorldPoint(screenPosition);
        worldPosition.z = 0; 
        return worldPosition;
    }

}
