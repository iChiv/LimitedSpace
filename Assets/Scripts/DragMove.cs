using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    public float slowSpeed;
    public float slowThreshold;
    private bool _isDragging = false;
    private Vector2 _slowSpeedV2;
    private Vector2 _initialMousePosition;
    private Vector3 _initialObjectPosition;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _slowSpeedV2 = new Vector2(slowSpeed, slowSpeed);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                _isDragging = true;
                _initialMousePosition = mousePosition;
                _initialObjectPosition = transform.position;
            }
        }

        if (_isDragging)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 positionOffset = currentMousePosition - _initialMousePosition;
            Vector2 newPosition = _initialObjectPosition + new Vector3(positionOffset.x, positionOffset.y, 0);
            _rb.MovePosition(newPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
        
        
        //Slow down after collision
        if (_rb.velocity.magnitude >= slowThreshold)
        {
            _rb.velocity -= _slowSpeedV2 * Time.deltaTime;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
        
    }
}
