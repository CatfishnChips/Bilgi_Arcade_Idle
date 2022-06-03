using UnityEngine;
using Assets.Scripts.Enums;
using DG.Tweening;
//using RayFire;

public class PlayerManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    #endregion

    #region Private Variables

    private PlayerMovementController movementController;
    private CameraController cameraController; //Should only communicate with cameraManager
    private PlayerPhysicsController physicsController;
    private AnimationController animationController;

    #endregion

    #endregion

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        cameraController = GetComponent<CameraController>();
        physicsController = GetComponentInChildren<PlayerPhysicsController>();
        animationController = GetComponentInChildren<AnimationController>();
    }

    private void OnEnable()
    {
        AssignEvents();
    }

    private void OnDisable()
    {
        UnAssignEvents();
    }
    
    private void AssignEvents()
    {
        EventManager.Instance.onInputDragged += OnInputDragged;
        EventManager.Instance.onInputTaken += OnInputTaken;
        EventManager.Instance.onInputReleased += OnInputReleased;
    }

    private void UnAssignEvents()
    {
        EventManager.Instance.onInputDragged -= OnInputDragged;
        EventManager.Instance.onInputTaken -= OnInputTaken;
        EventManager.Instance.onInputReleased -= OnInputReleased;
    }

    private void OnInputDragged(JoystickInputParams inputParameters)
    {  
        //Vector3 moveDirection = (cameraController.GetCameraRight() * inputParameters.HorizontalInputValue.x) + (cameraController.GetCameraForward() * -inputParameters.HorizontalInputValue.y);
        movementController.UpdateInputData(inputParameters);
        movementController.SetMovementAvailable();
        //animationController.ChangeWalkingMultiplier(moveDirection);

    }

    private void OnInputTaken() 
    {
        animationController.SetAnimationStateToWalk();
    }

    private void OnInputReleased()
    {
        animationController.SetAnimationStateToIdle();
        movementController.SetMovementUnAvailable();
    }

    public void ChangeAnimationState(AnimationStates states)
    {
        animationController.ChangeAnimationState(states);
    }

    //public void CutCuttable(RayfireRigid rigid)
    //{
    //    rigid.ApplyDamage(15, new Vector3(rigid.transform.position.x, 0, rigid.transform.position.z), 5);
    //}
}
