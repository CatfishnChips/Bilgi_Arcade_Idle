using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private float sensitivity;

    #endregion

    #region Private Variables

    private CameraController cameraController;
    private bool isRotating = false;
    private Vector3 mousePosRef, mouseOffset;

    #endregion

    #endregion

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    void Update()
    {
        MouseDrag();
    }

    private void MouseDrag()
    {
        if (Input.GetMouseButton(1))
        {
            if (!isRotating)
            {
                isRotating = true;
                mousePosRef = Input.mousePosition;
            }

            mouseOffset = (Input.mousePosition - mousePosRef);
            cameraController.RotateCamera(-mouseOffset.x * sensitivity);
            mousePosRef = Input.mousePosition;
        }
        else
        {
            if (isRotating) isRotating = false;
        }
    }
}
