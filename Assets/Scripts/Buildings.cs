using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Buildings : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int requiredResourceA;
    public int requiredResourceB;
    public int requiredResourceC;

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

    private void Awake()
    {
        _spaceShip = FindObjectOfType<SpaceShip>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        _originalPos = _rectTransform.anchoredPosition;
        _canvasGroup.alpha = 0.5f;
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
        _rectTransform.anchoredPosition = _originalPos;
    }

    private void BuildObject(Vector2 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0; 
        
        GameObject builtObject = Instantiate(building, worldPosition, Quaternion.identity);

        if (building.CompareTag("Shield") && _spaceShip != null)
        {
            builtObject.transform.SetParent(_spaceShip.transform, false);
            builtObject.transform.localPosition = Vector3.zero;

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
    }

    public void UpdateResources(int resourceTypeA, int resourceTypeB, int resourceTypeC)
    {
        currentResourceA = resourceTypeA;
        currentResourceB = resourceTypeB;
        currentResourceC = resourceTypeC;
        CanBuildOrNot();
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

        _canvasGroup.alpha = _canBuild ? 1.0f : 0.5f;
    }
}
