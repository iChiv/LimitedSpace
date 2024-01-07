using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
    public GameObject[] resourcePrefabs;
    [Range(0,3f)] public float spawnInterval = 3.0f;
    
    public float spawnAreaMin = 0.3f;
    public float spawnAreaMax = 0.7f;

    private Camera _mainCamera;
    private float _timer;
    
    public float minimumSpawnInterval = 0.5f; 
    public float intervalDecrease = 0.0125f; 

    private float _timeSinceStart;

    public int objectsCount;

    public int GetObjectsCountInLayer(int layer)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (var obj in allObjects)
        {
            if (obj.layer == layer)
            {
                count++;
            }
        }

        return count;
    }

    void Start()
    { 
        _mainCamera = Camera.main;
        _timer = spawnInterval;
    }

    void Update()
    {
        int layer = LayerMask.NameToLayer("Resources");
        objectsCount = GetObjectsCountInLayer(layer);
        // Debug.Log("Number of objects in layer " + LayerMask.LayerToName(layer) + ": " + objectsCount);

        _timeSinceStart += Time.deltaTime;
        _timer -= Time.deltaTime;
        if (_timer <= 0 && objectsCount< 20)
        {
            SpawnResource();
            _timer = Mathf.Max(minimumSpawnInterval,spawnInterval - _timeSinceStart * intervalDecrease);
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
            newResource.transform.parent = resourceParent.transform;
        }
        else
        {
            resourceParent = new GameObject("Resources");
            newResource.transform.parent = resourceParent.transform;
        }
        
    }

    //Vector2 GetRandomPositionInView()
    //{
    //    //float randomX = Random.Range(spawnAreaMin, spawnAreaMax);
    //    //float randomY = Random.Range(spawnAreaMin, spawnAreaMax);
        
    //    float randomX = Random.Range(0f, 1f);
    //    float randomY = Random.Range(0.8f, 1.0f);

    //    Vector2 viewportPoint = new Vector2(randomX, randomY);
        
    //    return _mainCamera.ViewportToWorldPoint(viewportPoint);

    //}

    Vector2 GetRandomPositionInView()
    {
        float randomX = Random.value; 
        float randomY = Random.value; 
        
        if (Random.value > 0.5f)
        {
            // left and right sides
            randomX = Random.value > 0.5f ? 0f : 1f;
        }
        else
        {
            // up and down sides
            randomY = Random.value > 0.5f ? 0.05f : 0.95f;
        }

        return _mainCamera.ViewportToWorldPoint(new Vector2(randomX, randomY));
    }
}
