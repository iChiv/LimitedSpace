using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STARTPosition : MonoBehaviour
{
    public GameObject targetPos; 
    private Vector2 _targetPosition; 
    public float snapDistance = 0.5f; 

    private DragMove _dragMove;
    private Rigidbody2D _rb;

    private void Start()
    {
        _targetPosition = targetPos.transform.position;
        Debug.Log(_targetPosition);
        _dragMove = GetComponent<DragMove>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_dragMove != null && !_dragMove._isDragging && Vector2.Distance(transform.position, _targetPosition) <= snapDistance)
        {
            _rb.MovePosition(_targetPosition);
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            
            transform.rotation = Quaternion.identity;
            
            GetComponent<DragMove>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            enabled = false; 
            // SceneManager.LoadScene("Game");
        }
    }

    public bool IsAtTargetPosition()
    {
        return Vector2.Distance(transform.position, _targetPosition) <= snapDistance;
    }
}
