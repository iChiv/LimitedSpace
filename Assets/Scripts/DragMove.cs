using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    [HideInInspector]public bool _isDragging = false;
    private Vector2 _initialMousePosition;
    private Vector3 _initialObjectPosition;
    private Rigidbody2D _rb;

    public AudioClip pickUp;
    public LayerMask buildingleLayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (pickUp != null)
                {
                    AudioSource.PlayClipAtPoint(pickUp,transform.position,1f);
                }
                _isDragging = true;
                _initialMousePosition = mousePosition;
                _initialObjectPosition = transform.position;
            }
        }

        if (_isDragging)
        {
            _rb.isKinematic = true;
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 positionOffset = currentMousePosition - _initialMousePosition;
            
            Vector2 targetPosition = _initialObjectPosition + new Vector3(positionOffset.x, positionOffset.y, 0);
            RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition, buildingleLayer);
            if (hit.collider == null)
            {
                _rb.MovePosition(targetPosition);
            }
            else
            {
                _isDragging = false;
            }
            
            // Vector2 newPosition = _initialObjectPosition + new Vector3(positionOffset.x, positionOffset.y, 0);
            // _rb.MovePosition(newPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_isDragging)
        {
            _rb.isKinematic = false;
            _rb.velocity *= 0.95f;
        }
        
        if (_rb.velocity.magnitude <= 0.3)
        {
            _rb.velocity = Vector2.zero;
        }
    }
}
