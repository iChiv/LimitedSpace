using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
    public GameObject[] resourcePrefabs;
    public float spawnInterval = 2.0f;
    public float spawnAreaMin = 0.2f;
    public float spawnAreaMax = 0.8f;

    private Camera _mainCamera;
    private float _timer;

    void Start()
    {
        _mainCamera = Camera.main;
        _timer = spawnInterval;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            SpawnResource();
            _timer = spawnInterval;
        }
    }

    void SpawnResource()
    {
        int resourceIndex = Random.Range(0, resourcePrefabs.Length);
        GameObject resourcePrefab = resourcePrefabs[resourceIndex];

        Vector2 spawnPosition = GetRandomPositionInView();
        GameObject newResource =  Instantiate(resourcePrefab, spawnPosition, Quaternion.identity);
        //need a short animation
        GameObject resourceParent = GameObject.Find("Resources");
        if (resourceParent != null)
        {
            newResource.transform.parent = GameObject.Find("Resources").transform;
        }
        else
        {
            resourceParent = new GameObject("Resources");
            newResource.transform.parent = GameObject.Find("Resources").transform;
        }
        
    }

    Vector2 GetRandomPositionInView()
    {
        float randomX = Random.Range(spawnAreaMin, spawnAreaMax);
        float randomY = Random.Range(spawnAreaMin, spawnAreaMax);
        Vector2 viewportPoint = new Vector2(randomX, randomY);
        
        return _mainCamera.ViewportToWorldPoint(viewportPoint);
    }
}
