using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SpaceShip : MonoBehaviour
{
    [Header("Resource Count")]
    private int _resourceCountTypeA = 0;
    private int _resourceCountTypeB = 0;
    private int _resourceCountTypeC = 0;

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

    void OnCollisionEnter2D(Collision2D other)
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
    }
    
    void AddResource(int level, string resourceType)
    {
        int points = 0;
        switch (level)
        {
            case 1: points = lv1Points; break;
            case 2: points = lv2Points; break;
            case 3: points = lv3Points; break;
        }

        if (resourceType == "ResourceTypeA")
        {
            _resourceCountTypeA += points;
            UpdateResourceCountDisplay(resourceCountTextTypeA, resourceNameTypeA, _resourceCountTypeA);
        }
        else if (resourceType == "ResourceTypeB")
        {
            _resourceCountTypeB += points;
            UpdateResourceCountDisplay(resourceCountTextTypeB, resourceNameTypeB, _resourceCountTypeB);
        }
        else if (resourceType == "ResourceTypeC")
        {
            _resourceCountTypeC += points;
            UpdateResourceCountDisplay(resourceCountTextTypeC, resourceNameTypeC, _resourceCountTypeC);
        }
    }
    
    void UpdateResourceCountDisplay(TextMeshProUGUI textMesh, string resourceName, int count)
    {
        if (textMesh != null)
        {
            textMesh.text = $"{resourceName}: {count}";
        }
    }
}
