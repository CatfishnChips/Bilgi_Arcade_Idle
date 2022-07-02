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
    [SerializeField] private List<Joystick> joysticks;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Joystick attackJoystick; 
    [SerializeField] private Joystick abilityJoystick1;
    [SerializeField] private Joystick abilityJoystick2;
    [SerializeField] private Joystick abilityJoystick3;

    #endregion

    #region Private Variables

    private bool _isTouching;

    private float _currentVelocity; //ref type
    private Vector2? _mousePosition; //ref type
    private Vector3 _moveVector; //ref type
    private bool _attackInputTaken, _ability1InputTaken, _ability2InputTaken, _ability3InputTaken;

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
            if (!IsAvailableForTouch) return;

            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;

                EventManager.Instance.onInputReleased?.Invoke();

                // OnAttackInputReleased
                if (_attackInputTaken == true) {
                    _attackInputTaken = false;
                    EventManager.Instance.onAttackInputReleased?.Invoke();
                }

                if (_ability1InputTaken == true) {
                    _ability1InputTaken = false;
                    EventManager.Instance.onAbility1InputReleased?.Invoke();
                }

                if (_ability2InputTaken == true) {
                    _ability2InputTaken = false;
                    EventManager.Instance.onAbility2InputReleased?.Invoke();
                }

                if (_ability3InputTaken == true) {
                    _ability3InputTaken = false;
                    EventManager.Instance.onAbility3InputReleased?.Invoke();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                EventManager.Instance.onInputTaken?.Invoke();
                if (!isFirstTimeTouchTaken)
                {
                    isFirstTimeTouchTaken = true;
                    //onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;

                if (IsPointerOverUIElement(out PointerEventData eventData, out List<RaycastResult> raycastResults)) {
                    foreach (var obj in raycastResults) {

                        if (obj.gameObject == attackJoystick.gameObject) 
                        {   
                            if (_attackInputTaken != true) _attackInputTaken = true;
                        } 
                        else if (obj.gameObject == abilityJoystick1.gameObject) {
                            if (_ability1InputTaken != true) _ability1InputTaken = true;
                        }   
                        else if (obj.gameObject == abilityJoystick2.gameObject) {
                            if (_ability2InputTaken != true) _ability2InputTaken = true;
                        }
                        else if (obj.gameObject == abilityJoystick3.gameObject) {
                            if (_ability3InputTaken != true) _ability3InputTaken = true;
                        }                   
                    }
                }   
            }

            if (Input.GetMouseButton(0))
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                        EventManager.Instance.onInputDragged?.Invoke(new JoystickInputParams()
                        {
                            DirectionInputValue = joystick.Direction,
                            HorizontalInputValue = joystick.Horizontal,
                            VerticalInputValue = joystick.Vertical,
                            HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
                            HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide              
                        });
                    }
                }
            }

            if (_attackInputTaken) {
                                
                EventManager.Instance.onAttackInputDragged?.Invoke(new JoystickInputParams()
                {   
                    DirectionInputValue = attackJoystick.Direction,
                    HorizontalInputValue = attackJoystick.Horizontal,
                    VerticalInputValue = attackJoystick.Vertical,
                    HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
                    HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide              
                });
            }    

            if (_ability1InputTaken) {
                                
                EventManager.Instance.onAbility1InputDragged?.Invoke(new JoystickInputParams()
                {   
                    DirectionInputValue = abilityJoystick1.Direction,
                    HorizontalInputValue = abilityJoystick1.Horizontal,
                    VerticalInputValue = abilityJoystick1.Vertical,
                    HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
                    HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide              
                });
            }    

            if (_ability2InputTaken) {
                                
                EventManager.Instance.onAbility2InputDragged?.Invoke(new JoystickInputParams()
                {   
                    DirectionInputValue = abilityJoystick2.Direction,
                    HorizontalInputValue = abilityJoystick2.Horizontal,
                    VerticalInputValue = abilityJoystick2.Vertical,
                    HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
                    HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide              
                });
            }    

            if (_ability3InputTaken) {
                                
                EventManager.Instance.onAbility3InputDragged?.Invoke(new JoystickInputParams()
                {   
                    DirectionInputValue = abilityJoystick3.Direction,
                    HorizontalInputValue = abilityJoystick3.Horizontal,
                    VerticalInputValue = abilityJoystick3.Vertical,
                    HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
                    HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide              
                });
            }    
        }

    public bool IsPointerOverUIElement(out PointerEventData eventData, out List<RaycastResult> raycastResults)
    {
        eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,raycastResults);
        return raycastResults.Count > 0;
    }

    private void OnAttackJoystickDown(PointerEventData eventData) {

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

    // private void JoystickInput() 
    // {
    //     Vector2 inputValue = joystick.Direction;

    //     EventManager.Instance.onInputDragged?.Invoke(new JoystickInputParams() {
    //         HorizontalInputValue = inputValue,
    //         HorizontalInputClampNegativeSide = InputData.Data.HorizontalInputClampNegativeSide,
    //         HorizontalInputClampPositiveSide = InputData.Data.HorizontalInputClampPositiveSide
    //     });

    //     if (inputValue != Vector2.zero) 
    //     {
    //         _isTouching = true;
    //         if (!isFirstTimeTouchTaken)
    //         EventManager.Instance.onInputTaken?.Invoke();
    //         isFirstTimeTouchTaken = true;
    //     }

    //     if (inputValue == Vector2.zero) 
    //     {
    //         if (!isFirstTimeTouchTaken) return;

    //         _isTouching = false;
    //         EventManager.Instance.onInputReleased?.Invoke();
    //         isFirstTimeTouchTaken = false;
    //     }

        //Debug.Log("Joystick Direction: " + joystick.Direction + " Joystick Horizontal: " + joystick.Horizontal + " Joystick Vertical: " + joystick.Vertical);
}
