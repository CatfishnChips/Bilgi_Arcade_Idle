using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Enums;

[CreateAssetMenu(fileName = "Lootable", menuName = ("ScriptableObjects/TriggerStayInteraction/Lootable"))]
public class LootableScriptable : TriggerStayScriptableBase
{
    private bool _isInCuttingState;
    private PlayerManager _playerManager;
    [SerializeField] private CollectableTypes _type;
    [SerializeField] private int _amountPerSwing;

    public override void ExecuteFunction(GameObject player, Collider other) {


        if (_playerManager == null) _playerManager = player.GetComponent<PlayerManager>();

         if (!_isInCuttingState)
            {
                _isInCuttingState = true;
                DOVirtual.DelayedCall(3, () =>
                {
                    _playerManager.UpdateInGameCurrency(_type, _amountPerSwing);
                }).OnComplete(() => _isInCuttingState = false);
            }
    }
}
