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
        // if (other.CompareTag("Cuttable"))
        // {
        //     if (!_isInCuttingState)
        //     {
        //         _isInCuttingState = true;
        //         DOVirtual.DelayedCall(3, () =>
        //         {
        //             playerManager.UpdateInGameCurrency(other.GetComponent<CollectableManager>().Type, 3);
        //             //manager.CutCuttable(other.transform.GetChild(0).transform.GetComponent<RayfireRigid>());
        //         }).OnComplete(() => _isInCuttingState = false);
        //     }
        // }

        //  if (other.CompareTag("Buyable"))
        // {
        //     var data = other.GetComponent<BuyableManager>().BuyableData.Data;
        //     //EconomyParams inGameEconomyParams = (EconomyParams)(EventManager.Instance.onGetInGameEconomyParams?.Invoke());
        //     FindObjectOfType<EconomyManager>().GetResources(out int wood, out int stone, out int gold);
        //     EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Wood, (int)(wood - data.WoodRequirement));
        //     EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Stone, (int)(stone - data.StoneRequirement));
        //     EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Gold, (int)(gold - data.GoldRequirement));
        //     if (wood < data.WoodRequirement) return;
        //     if (stone < data.StoneRequirement) return;
        //     if (gold < data.GoldRequirement) return;
        //     GameObject obj = Instantiate(data.PrefabReference, data.SpawnPosition, Quaternion.Euler(data.SpawnRotation), other.transform);

        // }
        if (other.CompareTag("Interactable")) 
        other.GetComponent<InteractionHolder>().Data.ExecuteFunction(this.gameObject, other);
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
