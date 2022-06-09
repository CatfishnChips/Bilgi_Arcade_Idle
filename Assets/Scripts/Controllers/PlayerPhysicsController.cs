using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using DG.Tweening;
//using RayFire;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Seriazlied Variables

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;

    #endregion

    #region Private Variables

    [ShowInInspector] private bool _isInCuttingState;

    #endregion

    #endregion

      private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Cuttable"))
        {
            playerManager.ChangeTheAnimationState(AnimationStates.Cut);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cuttable"))
        {
            if (!_isInCuttingState)
            {
                _isInCuttingState = true;
                DOVirtual.DelayedCall(3, () =>
                {
                    playerManager.UpdateInGameCurrency(other.GetComponent<CollectableManager>().Type, 3);
                    //manager.CutCuttable(other.transform.GetChild(0).transform.GetComponent<RayfireRigid>());
                }).OnComplete(() => _isInCuttingState = false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cuttable"))
        {
            playerManager.DisableAnimatorCuttingState();
            playerManager.ChangeTheAnimationState(AnimationStates.Idle);
        }
    }
}
