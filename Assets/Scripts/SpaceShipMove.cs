using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceShipMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 300f;
    public float maxAngularVelocity = 100.0f;
    public GameObject engineFire;
    private Rigidbody2D _rb;

    private float _currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");

        //Test of moving instead of turning
        // Vector2 moveDirection = new Vector2(speedX, speedY).normalized;
        //
        // if (moveDirection != Vector2.zero)
        // {
        //     float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        //     float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.fixedDeltaTime);
        //     
        //     _rb.MoveRotation(angle);
        //     _rb.MovePosition(_rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        // }
        
        if (speedY != 0)
        {
            _rb.AddForce(transform.up * speedY * moveSpeed);
            engineFire.SetActive(true);
        }
        else
        {
            engineFire.SetActive(false);
        }
        
        _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
        
        if (speedX != 0 && Mathf.Abs(_rb.angularVelocity) < maxAngularVelocity)
        {
            _rb.AddTorque(-speedX * turnSpeed);
        }
    }

    private void Update()
    {
        
    }
}
