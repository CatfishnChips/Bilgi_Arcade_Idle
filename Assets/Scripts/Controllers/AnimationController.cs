using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serizalized Variables

    [SerializeField] private Animator animator;

    #endregion

    #endregion

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeWalkingState(bool state) 
    {
        animator.SetBool("Walking", state);
    }

    public void ChangeWalkingMultiplier(Vector3 direction) 
    {
        float distance = Mathf.Sqrt(Mathf.Pow((direction.x), 2) + Mathf.Pow((direction.z), 2));
        animator.SetFloat("WalkMultiplier", distance);
    }
}
