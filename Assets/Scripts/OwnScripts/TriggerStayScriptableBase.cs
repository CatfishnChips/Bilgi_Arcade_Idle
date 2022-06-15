using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerStayScriptableBase : ScriptableObject
{
    public abstract void ExecuteFunction(GameObject player, Collider other);
}
