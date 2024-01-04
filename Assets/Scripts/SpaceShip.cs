using System;
using UnityEngine;
using TMPro;


public class SpaceShip : AsteroidImpact
{
    public float health;
    
    [Header("Resource Count")]
    public int resourceCountTypeA = 0;
    public int resourceCountTypeB = 0;
    public int resourceCountTypeC = 0;

    [Header("Resource Level Points")] 
    public int lv1Points = 1;
    public int lv2Points = 5;
    public int lv3Points = 30;
    
    [Header("Resource Display")]
    public string resourceNameTypeA = "Square";
    public string resourceNameTypeB = "Circle";
    public string resourceNameTypeC = "Triangle";
    public TextMeshProUGUI resourceCountTextTypeA;
    public TextMeshProUGUI resourceCountTextTypeB;
    public TextMeshProUGUI resourceCountTextTypeC;

    public Buildings[] buildings;

    private void Start()
    {
        hp = health;
    }

    new void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ResourceTypeA") ||
            other.gameObject.CompareTag("ResourceTypeB") ||
            other.gameObject.CompareTag("ResourceTypeC"))
        {
            Resources resources = other.gameObject.GetComponent<Resources>();
            if (resources != null)
            {
                AddResource(resources.resourceLevel, other.gameObject.tag);
            }
            Destroy(other.gameObject);
            //Play some sound
            //Cool VFX
            //Maybe Animation
        }
        
        base.OnCollisionEnter2D(other);
    }
    
    protected override void DestoryObject()
    {
        Destroy(gameObject);
        //Game Over
    }
    
    void AddResource(int level, string resourceType)
    {
        int points = 0;
        switch (level)
        {
            case 1: points = lv1Points; break;
            case 2: points = lv2Points; break;
            case 3: points = lv3Points; break;
            default: points = 0; break;
        }

        if (resourceType == "ResourceTypeA")
        {
            resourceCountTypeA += points;
            UpdateResourceCountDisplay(resourceCountTextTypeA, resourceNameTypeA, resourceCountTypeA);
        }
        else if (resourceType == "ResourceTypeB")
        {
            resourceCountTypeB += points;
            UpdateResourceCountDisplay(resourceCountTextTypeB, resourceNameTypeB, resourceCountTypeB);
        }
        else if (resourceType == "ResourceTypeC")
        {
            resourceCountTypeC += points;
            UpdateResourceCountDisplay(resourceCountTextTypeC, resourceNameTypeC, resourceCountTypeC);
        }

        foreach (var t in buildings)
        {
            if (t != null)
            {
                t.UpdateResources(resourceCountTypeA,resourceCountTypeB,resourceCountTypeC);
            }
        }
    }
    
    void UpdateResourceCountDisplay(TextMeshProUGUI textMesh, string resourceName, int count)
    {
        if (textMesh != null)
        {
            textMesh.text = $"{resourceName}: {count}";
        }
    }

    public void ResourceConsume()
    {
        UpdateResourceCountDisplay(resourceCountTextTypeA, resourceNameTypeA, resourceCountTypeA);
        UpdateResourceCountDisplay(resourceCountTextTypeB, resourceNameTypeB, resourceCountTypeB);
        UpdateResourceCountDisplay(resourceCountTextTypeC, resourceNameTypeC, resourceCountTypeC);
        foreach (var t in buildings)
        {
            if (t != null)
            {
                t.UpdateResources(resourceCountTypeA,resourceCountTypeB,resourceCountTypeC);
            }
        }
    }
}
