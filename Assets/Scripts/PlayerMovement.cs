using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float speed;

    private Vector2 inputValue;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputValue = InputData;
    }

    //private Vector2 GetInputData()
    //{
    //    return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //}

    private Vector2 InputData => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    private void FixedUpdate()
    {
        Move(inputValue, speed);
    }

    private void Move(Vector2 inputValue, float speed)
    {
        rigidbody.velocity = new Vector3(inputValue.x * speed * Time.fixedDeltaTime, rigidbody.velocity.y, inputValue.y * speed * Time.fixedDeltaTime);
    }
}
