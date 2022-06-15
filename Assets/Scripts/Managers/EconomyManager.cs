using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{

    #region Singleton 

    public static EconomyManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion

    #region Self Variables

    #region Public Variables

    CollectableTypes Types;

    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables
    [ShowInInspector] private int _wood, _gold, _stone;

    #endregion

    #endregion

    private void Start()
    {
        SubscribeEvents();

    }

    private void SubscribeEvents()
    {
        EventManager.Instance.onUpdateCollectableType += OnUpdateCollectableType;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.onUpdateCollectableType -= OnUpdateCollectableType;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnUpdateCollectableType(CollectableTypes type, int value)
    {

        switch (type)
        {
            case CollectableTypes.Wood:
                {
                    _wood += value;
                    EventManager.Instance.onUpdateUICollectableType?.Invoke(type, _wood);
                    break;
                }
            case CollectableTypes.Stone:
                {
                    _stone += value;
                    EventManager.Instance.onUpdateUICollectableType?.Invoke(type, _stone);
                    break;
                }
            case CollectableTypes.Gold:
                {
                    _gold += value;
                    EventManager.Instance.onUpdateUICollectableType?.Invoke(type, _gold);
                    break;
                }
        }
    }

    public void GetResources(out int wood, out int stone, out int gold)
    {
         wood = _wood;
         stone = _stone;
         gold = _gold;

    }
}
