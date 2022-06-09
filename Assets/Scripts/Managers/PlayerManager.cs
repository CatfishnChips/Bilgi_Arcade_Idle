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
        EventManager.Instance.onInputTaken += OnInputTaken;
        EventManager.Instance.onInputDragged += OnInputDragged;
        EventManager.Instance.onInputReleased += OnInputReleased;
    }



    private void UnAssignEvents()
    {
        EventManager.Instance.onInputTaken -= OnInputTaken;
        EventManager.Instance.onInputDragged -= OnInputDragged;
        EventManager.Instance.onInputReleased -= OnInputReleased;
    }

    private void OnInputTaken()
    {
        animationController.ChangeAnimationState(AnimationStates.Walk);
    }

    private void OnInputDragged(JoystickInputParams inputParams)
    {
        movementController.SetMovementAvailable();
        movementController.UpdateInputData(inputParams);
    }

    private void OnInputReleased()
    {
        movementController.SetMovementUnAvailable();
        animationController.ChangeAnimationState(AnimationStates.Idle);
    }



    public void ChangeTheAnimationState(AnimationStates states)
    {
        animationController.ChangeAnimationState(states);
    }

    //public void CutCuttable(RayfireRigid rigid)
    //{
    //    rigid.ApplyDamage(15, new Vector3(rigid.transform.position.x, 0, rigid.transform.position.z), 5);
    //}

    public void UpdateInGameCurrency(CollectableTypes type, int value)
    {
        EventManager.Instance.onUpdateCollectableType?.Invoke(type, value);
    }

    public void DisableAnimatorCuttingState()
    {
        animationController.DisableAnimatorCuttingState();
    }
}
