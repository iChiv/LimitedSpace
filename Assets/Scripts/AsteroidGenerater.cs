using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerater : MonoBehaviour
{
    public GameObject asteroidPrefab;

    [Range(0.01f,5)]
    public float spawnInterval = 1f;


    void Start()
    {
        InvokeRepeating("SpawnAsteroid", spawnInterval, spawnInterval);

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
