using System;
using UnityEngine;

[Serializable]
public class BuyableData
{
    public GameObject PrefabReference;
    public Vector3 SpawnPosition, SpawnRotation;
    public float WoodRequirement, StoneRequirement, GoldRequirement;
}
