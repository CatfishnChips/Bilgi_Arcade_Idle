using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Self Variables

    #region Private Variables

    private Vector3 _moveDirections;
    private bool _isReadyToMove;

    #endregion

    #region Serialized Variables

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private MovementTypes movementOptions;

    #endregion

    #endregion

    private void FixedUpdate()
    {
        if (_isReadyToMove)
        {
            switch (movementOptions)
            {
                case MovementTypes.Velocity:
                    MovePlayerVelocity();
                    break;
                case MovementTypes.AddForce:
                    MovePlayerAddForce();
                    break;
                case MovementTypes.Transform:
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
        _moveDirections = moveDirection;
    }

    #region Velocity Movement

    private void MovePlayerVelocity()
    {
        rigidbody.velocity = new Vector3(_moveDirections.x * _speed, rigidbody.velocity.y, _moveDirections.z * _speed);
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
        rigidbody.AddForce(new Vector3(_moveDirections.x * 1.5f * _speed, 0, _moveDirections.z * 1.5f *  _speed), ForceMode.Force);
    }

    #endregion

    #region Transform Movement

    private void MovePlayerTransform()
    {
        transform.position += new Vector3(_moveDirections.x * 0.025f *_speed, 0, _moveDirections.z * 0.025f * _speed);
    }

    #endregion
}
