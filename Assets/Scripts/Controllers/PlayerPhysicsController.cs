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

        if (other.CompareTag("Buyable"))
        {
            var buyableManager = other.GetComponent<BuyableManager>();
            var data = other.GetComponent<BuyableManager>().BuyableData;
            var priceresultWood = data.WoodRequirement - EconomyManager._wood;
            var priceresultStone = data.StoneRequirement - EconomyManager._stone;
            var priceresultGold = data.GoldRequirement - EconomyManager._gold;
            if (priceresultWood <= 0 && priceresultStone <= 0 && priceresultGold <= 0 && !data.IsBought)
            {
                Debug.LogWarning(priceresultWood);
                EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Wood, -(EconomyManager._wood + priceresultWood));
                EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Stone, -(EconomyManager._stone + priceresultStone));
                EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Gold, -(EconomyManager._gold + priceresultGold));
                buyableManager.BuyTheObject();
            }
            else { }


            if (priceresultWood > 0)
            {
                data.WoodRequirement -= EconomyManager._wood;
                EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Wood, -(EconomyManager._wood + priceresultWood));
            }

            if (priceresultStone > 0)
            {

                data.StoneRequirement -= EconomyManager._stone;
                EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Stone, -(EconomyManager._stone + priceresultStone));
            }

            if (priceresultGold > 0)
            {

                data.GoldRequirement -= EconomyManager._stone;
                EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Gold, -(EconomyManager._gold + priceresultGold));
            }
        }
        // if (other.CompareTag("Interactable")) 
        // other.GetComponent<InteractionHolder>().Data.ExecuteFunction(this.gameObject, other);
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
