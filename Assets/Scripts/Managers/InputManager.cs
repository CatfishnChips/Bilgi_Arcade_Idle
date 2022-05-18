using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("Data")] public CD_Inputs InputData;
    [Header("Additional Variables")] public bool IsAvailableForTouch;

    #endregion

    #region Serialized Variables

    [SerializeField] private bool isFirstTimeTouchTaken;
    [SerializeField] private Joystick joystick;

    #endregion

    #region Private Variables

    private bool _isTouching;

    private float _currentVelocity; //ref type
    private Vector2? _mousePosition; //ref type
    private Vector3 _moveVector; //ref type

    #endregion

    #endregion

    private void OnEnable()
    {
        EventManager.Instance.onReset += ResetData;
        EventManager.Instance.onPlay += OnPlay;
    }

    private void OnDisable()
    {
        EventManager.Instance.onReset -= ResetData;
        EventManager.Instance.onPlay -= OnPlay;
    }

    private void Update()
    {
        JoystickInput();

        if (!IsAvailableForTouch) return;

        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
        {
            _isTouching = false;

            EventManager.Instance.onInputReleased?.Invoke();
        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            _isTouching = true;
            if (!isFirstTimeTouchTaken)
            {
                EventManager.Instance.onInputTaken?.Invoke();
            }

            _mousePosition = Input.mousePosition;
            Debug.Log(_mousePosition);
        }

        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                    if (mouseDeltaPos.x > InputData.Data.HorizontalInputSpeed)
                        _moveVector.x = InputData.Data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    else if (mouseDeltaPos.x < -InputData.Data.HorizontalInputSpeed)
                        _moveVector.x = -InputData.Data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                    else
                        _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, InputData.Data.HorizontalInputClampStopValue);

                    _mousePosition = Input.mousePosition;

                    EventManager.Instance.onInputDragged?.Invoke(new HorizontalInputParams() {
                        HorizontalInputValue = _moveVector,
                        HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide,
                        HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide
                    });
                    
                }
            }
        }

    }

    public bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,results);
        return results.Count > 0;
    }

    private void OnPlay()
    {
        IsAvailableForTouch = true;
    }

    private void ResetData()
    {
        _isTouching = false;
        isFirstTimeTouchTaken = false;
    }

    private void JoystickInput() 
    {
        Vector2 inputValue = joystick.Direction;

        EventManager.Instance.onInputDragged?.Invoke(new HorizontalInputParams() {
            HorizontalInputValue = inputValue,
            HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
            HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide
        });

        if (inputValue != Vector2.zero) 
        {
            _isTouching = true;
            if (!isFirstTimeTouchTaken)
            EventManager.Instance.onInputTaken?.Invoke();
            isFirstTimeTouchTaken = true;
        }

        if (inputValue == Vector2.zero) 
        {
            if (!isFirstTimeTouchTaken) return;

            _isTouching = false;
            EventManager.Instance.onInputReleased?.Invoke();
            isFirstTimeTouchTaken = false;
        }

        //Debug.Log("Joystick Direction: " + joystick.Direction + " Joystick Horizontal: " + joystick.Horizontal + " Joystick Vertical: " + joystick.Vertical);
    }
}