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
        Debug.Log("Number of objects in layer " + LayerMask.LayerToName(layer) + ": " + objectsCount);

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
            newResource.transform.parent = GameObject.Find("Resources").transform;
        }
        else
        {
            resourceParent = new GameObject("Resources");
            newResource.transform.parent = GameObject.Find("Resources").transform;
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
        float randomX = Random.value; // 0 到 1 之间的随机值
        float randomY = Random.value; // 0 到 1 之间的随机值

        // 随机选择固定 X 或 Y 坐标到边缘
        if (Random.value > 0.5f)
        {
            // 固定 X 坐标到左或右边缘
            randomX = Random.value > 0.5f ? 0.05f : 0.95f;
        }
        else
        {
            // 固定 Y 坐标到上或下边缘
            randomY = Random.value > 0.5f ? 0.05f : 0.95f;
        }

        return _mainCamera.ViewportToWorldPoint(new Vector2(randomX, randomY));
    }
}
