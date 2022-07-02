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
    private PlayerActionController actionController;
    private PlayerHealthController healthController;

    #endregion

    #endregion

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        cameraController = GetComponent<CameraController>();
        physicsController = GetComponentInChildren<PlayerPhysicsController>();
        animationController = GetComponentInChildren<AnimationController>();
        actionController = GetComponentInChildren<PlayerActionController>();
        healthController = GetComponent<PlayerHealthController>();
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

        EventManager.Instance.onAttackInputDragged += OnAttackInputDragged;
        EventManager.Instance.onAttackInputReleased += OnAttackInputReleased;

        EventManager.Instance.onAbility1InputDragged += OnAbility1InputDragged;
        EventManager.Instance.onAbility1InputReleased += OnAbility1InputReleased;

        EventManager.Instance.onAbility2InputDragged += OnAbility2InputDragged;
        EventManager.Instance.onAbility2InputReleased += OnAbility2InputReleased;

        EventManager.Instance.onAbility3InputDragged += OnAbility3InputDragged;
        EventManager.Instance.onAbility3InputReleased += OnAbility3InputReleased;
    }



    private void UnAssignEvents()
    {
        EventManager.Instance.onInputTaken -= OnInputTaken;
        EventManager.Instance.onInputDragged -= OnInputDragged;
        EventManager.Instance.onInputReleased -= OnInputReleased;

        EventManager.Instance.onAttackInputDragged -= OnAttackInputDragged;
        EventManager.Instance.onAttackInputReleased -= OnAttackInputReleased;

         EventManager.Instance.onAbility1InputDragged -= OnAbility1InputDragged;
        EventManager.Instance.onAbility1InputReleased -= OnAbility1InputReleased;

        EventManager.Instance.onAbility2InputDragged -= OnAbility2InputDragged;
        EventManager.Instance.onAbility2InputReleased -= OnAbility2InputReleased;

        EventManager.Instance.onAbility3InputDragged -= OnAbility3InputDragged;
        EventManager.Instance.onAbility3InputReleased -= OnAbility3InputReleased;
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

    private void OnAttackInputDragged(JoystickInputParams inputParams) {
        actionController.AimAttack(inputParams);
    }

    private void OnAttackInputReleased() {
        actionController.ExecuteAttack();
        //animationController.ChangeAnimationState(AnimationStates.Attack);
    }
    private void OnAbility1InputDragged(JoystickInputParams inputParams) {
        actionController.AimAbility1(inputParams);
    }

    private void OnAbility1InputReleased() {
        actionController.ExecuteAbility1();
    }
    private void OnAbility2InputDragged(JoystickInputParams inputParams) {
        actionController.AimAbility2(inputParams);
    }

    private void OnAbility2InputReleased() {
        actionController.ExecuteAbility2();
    }
    private void OnAbility3InputDragged(JoystickInputParams inputParams) {
        actionController.AimAbility3(inputParams);
    }

    private void OnAbility3InputReleased() {
        actionController.ExecuteAbility3();
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

    public void HandleDeath() {
        EventManager.Instance.onPlayerDeath.Invoke();
    }
}
