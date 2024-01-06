using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Buildings : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int requiredResourceA;
    public int requiredResourceB;
    public int requiredResourceC;

    public TextMeshProUGUI textResourceA;
    public TextMeshProUGUI textResourceB;
    public TextMeshProUGUI textResourceC;
    public float consumeResourceIncrease = 1.5f;

    [SerializeField] private int currentResourceA;
    [SerializeField] private int currentResourceB;
    [SerializeField] private int currentResourceC;

    private bool _canBuild = false;

    private SpaceShip _spaceShip;
    
    public GameObject building;
    public GameObject uiPanelBuilding;
    
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector2 _originalPos;

    public TextMeshProUGUI resourceADisplay;
    public TextMeshProUGUI resourceBDisplay;
    public TextMeshProUGUI resourceCDisplay;

    private float _currentRotation = 0f;

    private void Awake()
    {
        _spaceShip = FindObjectOfType<SpaceShip>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        _originalPos = _rectTransform.anchoredPosition;
        // _canvasGroup.alpha = 0.5f;
    }

    private void Update()
    {
        if (_canBuild && Input.GetKeyDown(KeyCode.R))
        {
            RotateBuilding();
        }
    }

    private void RotateBuilding()
    {
        _currentRotation += 90f;
        if (_currentRotation >= 360f)
        {
            _currentRotation = 0f;
        }

        _rectTransform.localEulerAngles = new Vector3(0, 0, _currentRotation);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_canBuild) return;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_canBuild) return;
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        uiPanelBuilding.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_canBuild) return;
        _canvasGroup.blocksRaycasts = true;
        BuildObject(eventData.position);
        ResetBuildingPreview();
    }

    private void ResetBuildingPreview()
    {
        _rectTransform.anchoredPosition = _originalPos;
        _rectTransform.localEulerAngles =  Vector3.zero;
        _currentRotation = 0f;
    }

    private void BuildObject(Vector2 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        if (CanPlaceOrNot(worldPosition))
        {
            GameObject builtObject = Instantiate(building, worldPosition, Quaternion.identity);

            builtObject.transform.rotation = Quaternion.Euler(0,0,_currentRotation);
        
            if (building.CompareTag("Shield") && _spaceShip != null)
            {
                builtObject.transform.SetParent(_spaceShip.transform, false);
                builtObject.transform.localPosition = new Vector3(0, -0.6f,0);

                // BuildingShield newShield = builtObject.GetComponent<BuildingShield>();
                // if (newShield != null)
                // {
                //     newShield.AdjustShieldSize();
                // }
            }
        
            if (_spaceShip != null)
            {
                _spaceShip.resourceCountTypeA -= requiredResourceA;
                _spaceShip.resourceCountTypeB -= requiredResourceB;
                _spaceShip.resourceCountTypeC -= requiredResourceC;
                _spaceShip.ResourceConsume();
            }

            requiredResourceA = requiredResourceA < 32 ? Mathf.FloorToInt(requiredResourceA * consumeResourceIncrease) : 32;
            requiredResourceB = requiredResourceB < 24 ? Mathf.FloorToInt(requiredResourceB * consumeResourceIncrease) : 24;
            requiredResourceC = requiredResourceC < 32 ? Mathf.FloorToInt(requiredResourceC * consumeResourceIncrease) : 32;
            
        
            UpdateResourceTexts();
        }
        else
        {
                // 如果不能建造，重置UI位置
                ResetBuildingPreview();
        }
    }

    public void UpdateResources(int resourceTypeA, int resourceTypeB, int resourceTypeC)
    {
        currentResourceA = resourceTypeA;
        currentResourceB = resourceTypeB;
        currentResourceC = resourceTypeC;
        CanBuildOrNot();
        UpdateResourceTexts();
    }

    private void UpdateResourceTexts()
    {
        if (textResourceA != null)
            textResourceA.text = requiredResourceA.ToString();
        if (textResourceB != null)
            textResourceB.text = requiredResourceB.ToString();
        if (textResourceC != null)
            textResourceC.text = requiredResourceC.ToString();
    }

    void CanBuildOrNot()
    {
        if (currentResourceA >= requiredResourceA && 
            currentResourceB >= requiredResourceB &&
            currentResourceC >= requiredResourceC)
        {
            _canBuild = true;
        }
        else
        {
            _canBuild = false;
        }

        _canvasGroup.alpha = _canBuild ? 1.0f : 0.1f;
        
        
        if (resourceADisplay != null)
        {
            resourceADisplay.alpha = _canBuild ? 1.0f : 0.2f;
        }
        if (resourceBDisplay != null)
        {
            resourceBDisplay.alpha = _canBuild ? 1.0f : 0.2f;
        }
        if (resourceCDisplay != null)
        {
            resourceCDisplay.alpha = _canBuild ? 1.0f : 0.2f;
        }
    }

    private bool CanPlaceOrNot(Vector3 position)
    {
        float checkRadius = 0.5f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, checkRadius, LayerMask.GetMask("Building"));
        return colliders.Length == 0;
    }
}
