using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class AnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serizalized Variables

    [Header("Manager")] [SerializeField] private PlayerManager manager;
    [Space]
    [SerializeField] private Animator animator;

    #endregion

    #region Private Variables

    private const string Walk = "Walk";
    private const string Idle = "Idle";
    private const string Cut = "Cut";

    #endregion

    #endregion

    private void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void DisableAnimatorCuttingState()
    {
        animator.SetBool(Cut, false);
    }

    public void ChangeAnimationState(AnimationStates states)
    {
        switch (states)
        {
            case AnimationStates.Cut:
                {
                    animator.SetBool(Cut, true);
                    break;

                }
            case AnimationStates.Idle:
                {
                    animator.SetTrigger(Idle);
                    break;
                }
            case AnimationStates.Walk:
                {
                    animator.SetTrigger(Walk);
                    break;
                }

        }
    }
}
