using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private CameraController cameraController;

    #endregion

    #region Private Variables

    private Vector3 moveDirection;

    #endregion

    #endregion

    private void Update()
    {
        if (Input.anyKey)
        {
            moveDirection = (cameraController.GetCameraRight() * InputData.x) + (cameraController.GetCameraForward() * -InputData.y); 
            movementController.SetMovementAvailable();
            movementController.UpdateInputData(moveDirection);
        }
        else
        {
            movementController.SetMovementUnAvailable();
        }
    }

    private Vector2 InputData => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}
