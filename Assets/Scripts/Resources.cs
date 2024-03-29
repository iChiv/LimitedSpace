using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : AsteroidImpact
{
    public enum ResourceType
    {
        TypeA, TypeB, TypeC,
        TypeA2, TypeB2, TypeC2,
        TypeA3, TypeB3, TypeC3
    }

    public float health;
    public int resourceLevel;
    public ResourceType resourceType;
    public float detectionRadius = 2.0f; 
    public GameObject bigResourcePrefab;
    
    public AudioClip mergeSound;

    private void Start()
    {
        hp = health;
    }

    private void OnMouseUp()
    {
        List<GameObject> sameTypeResources = new List<GameObject>();
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Resources resource = hitCollider.GetComponent<Resources>();
            if (resource != null && resource.resourceType == this.resourceType)
            {
                sameTypeResources.Add(resource.gameObject);
            }
        }
        
        if (sameTypeResources.Count >= 2)
        {
            List<GameObject> resourcesToMerge = sameTypeResources.GetRange(0, 2);
            MergeResources(sameTypeResources);
        }
    }

    private void MergeResources(List<GameObject> resourcesToMerge)
    {
        GameObject newResource = Instantiate(bigResourcePrefab, transform.position, Quaternion.identity);
        newResource.transform.parent = GameObject.Find("Resources").transform;
        if (mergeSound != null)
        {
            AudioSource.PlayClipAtPoint(mergeSound, transform.position, volume);
        }
        
        foreach (var resource in resourcesToMerge)
        {
            if (resource != gameObject)
            {
                Destroy(resource);
            }
        }
        Destroy(gameObject);
    }

    protected override void DestoryObject()
    {
        base.DestoryObject();
        Destroy(gameObject);
    }
}
