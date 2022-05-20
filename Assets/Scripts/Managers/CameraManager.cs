using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System;

public class CameraManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform playerManager;

    #endregion

    #region Private Variables

    #endregion

    #endregion

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventManager.Instance.onPlay += OnAssignCameraTarget;
    }

    private void UnSubscribeEvents()
    {
        EventManager.Instance.onPlay -= OnAssignCameraTarget;
    }

    #endregion

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>().transform;
    }

    private void OnAssignCameraTarget()
    {
        virtualCamera.Follow = playerManager;
        virtualCamera.LookAt = playerManager;
    }

    // Input part should have its own script called Input Manager.

    #region Old Code

    //[SerializeField] private float sensitivity;
    //private CameraController cameraController;
    //private bool isRotating = false;
    //private Vector3 mousePosRef, mouseOffset;

    //private void MouseDrag()
    //{
    //    if (Input.GetMouseButton(1))
    //    {
    //        if (!isRotating)
    //        {
    //            isRotating = true;
    //            mousePosRef = Input.mousePosition;
    //        }

    //        mouseOffset = (Input.mousePosition - mousePosRef);
    //        cameraController.RotateCamera(-mouseOffset.x * sensitivity);
    //        mousePosRef = Input.mousePosition;
    //    }
    //    else
    //    {
    //        if (isRotating) isRotating = false;
    //    }
    //}

    #endregion
}
