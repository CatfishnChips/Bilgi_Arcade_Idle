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

    // public void ChangeWalkingState(bool state) 
    // {
    //     animator.SetBool("Walking", state);
    // }

    // public void ChangeWalkingMultiplier(Vector3 direction) 
    // {
    //     float distance = Mathf.Sqrt(Mathf.Pow((direction.x), 2) + Mathf.Pow((direction.z), 2));
    //     animator.SetFloat("WalkMultiplier", distance);
    // }

    public void SetAnimationStateToWalk()
    {
        animator.SetTrigger("Walking");
    }

    public void SetAnimationStateToIdle()
    {
        animator.SetTrigger("Idle");
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
                    animator.SetBool(Cut, false);
                    break;
                }
            case AnimationStates.Walk:
                {
                    animator.SetTrigger("Idle");
                    break;
                }

        }
    }
}
