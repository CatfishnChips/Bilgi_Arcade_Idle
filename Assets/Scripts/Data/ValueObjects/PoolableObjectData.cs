using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolableObjectData
{
    public GameObject Prefab;
    public int InitialPoolAmount;
    [HideInInspector] public Queue<GameObject> QueueReference;
    public int Cost;
}
