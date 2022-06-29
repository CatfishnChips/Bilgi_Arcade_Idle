using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

[CreateAssetMenu(fileName = "Buyable", menuName = ("ScriptableObjects/TriggerStayInteraction/Buyable"))]
public class BuyableScriptable : TriggerStayScriptableBase
{
    public BuyableData Data;

    public override void ExecuteFunction(GameObject player, Collider other)
    {
        FindObjectOfType<EconomyManager>().GetResources(out int wood, out int stone, out int gold);
        if (wood < Data.WoodRequirement) return;
        if (stone < Data.StoneRequirement) return;
        if (gold < Data.GoldRequirement) return;
        EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Wood, (int)(wood - Data.WoodRequirement));
        EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Stone, (int)(stone - Data.StoneRequirement));
        EventManager.Instance.onUpdateCollectableType?.Invoke(CollectableTypes.Gold, (int)(gold - Data.GoldRequirement));
        //GameObject obj = Instantiate(Data.PrefabReference, Data.SpawnPosition, Quaternion.Euler(Data.SpawnRotation), other.transform);
        other.enabled = false;
    }
}
