using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject asteroidGenerator; 
    public float interval = 60f; 
    private float _timer; 
    public int maxGenerators = 3; 
    private int _currentGenerators = 1; 

    void Start()
    {
        _timer = interval;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        
        if (_timer <= 0f && _currentGenerators < maxGenerators)
        {
            Instantiate(asteroidGenerator, Vector3.zero, Quaternion.identity);
            _currentGenerators++;
            _timer = interval;
        }
    }
}
