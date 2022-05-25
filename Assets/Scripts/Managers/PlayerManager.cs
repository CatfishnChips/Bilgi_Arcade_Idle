using UnityEngine;

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
}
