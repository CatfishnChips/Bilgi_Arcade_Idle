using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Serializable]
    public enum MovementOptions { Velocity, AddForce, Transform };

    #region Self Variables

    #region Private Variables

    private Vector3 moveDirections;
    private bool _isReadyToMove;

    #endregion

    #region Serialized Variables

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private MovementOptions movementOptions;

    #endregion

    #endregion

    private void FixedUpdate()
    {
        if (_isReadyToMove)
        {
            switch (movementOptions)
            {
                case MovementOptions.Velocity:
                    MovePlayerVelocity();
                    break;
                case MovementOptions.AddForce:
                    MovePlayerAddForce();
                    break;
                case MovementOptions.Transform:
                    MovePlayerTransform();
                    break;
            };
        }     
         else StopPlayerVelocity();
    }

    public void SetMovementAvailable()
    {
        _isReadyToMove = true;
    }

    public void SetMovementUnAvailable()
    {
        _isReadyToMove = false;
    }

    public void UpdateInputData(Vector3 moveDirection)
    {
        moveDirections = moveDirection;
    }

    #region Velocity Movement

    private void MovePlayerVelocity()
    {
        rigidbody.velocity = new Vector3(moveDirections.x * speed, rigidbody.velocity.y, moveDirections.z * speed);
    }

    private void StopPlayerVelocity()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        rigidbody.angularVelocity = Vector3.zero;
    }
    #endregion

    #region AddForce Movement
    private void MovePlayerAddForce()
    {
        rigidbody.AddForce(new Vector3(moveDirections.x * 1.5f * speed, 0, moveDirections.z * 1.5f *  speed), ForceMode.Force);
    }

    #endregion

    #region Transform Movement

    private void MovePlayerTransform()
    {
        transform.position += new Vector3(moveDirections.x * 0.025f *speed, 0, moveDirections.z * 0.025f * speed);
    }

    #endregion

    //HOMEWORK 
    //WRITE THIS CODE WITH ADDFORCE
    //WRITE THIS CODE WITH BY CHANGING TRANSFORM
    //USE GITHUB

}
