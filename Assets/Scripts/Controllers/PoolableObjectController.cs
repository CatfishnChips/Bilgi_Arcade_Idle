using UnityEngine;

public class PoolableObjectController : MonoBehaviour
{
    [HideInInspector] public bool IsCalledByPooling;

    public void EnqueueCheck(float timer)
    {
        if (IsCalledByPooling)
        {
            Invoke(nameof(Enqueue), timer);
        }
    }

    private void Enqueue()
    {
        IsCalledByPooling = false;
        ObjectPoolingManager.Instance.EnqueueObject(gameObject);
    }
}
