using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    #region Singleton

        public static ObjectPoolingManager Instance;

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

    
    public List<PoolableObjectData> PoolableObjectList;
    [SerializeField] private List<Queue<GameObject>> _poolableObjectQueue = new List<Queue<GameObject>>();

    public void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < PoolableObjectList.Count; i++) {
            PoolableObjectData data = PoolableObjectList[i];
            Queue<GameObject> queue = new Queue<GameObject>();
            _poolableObjectQueue.Add(queue);
            data.QueueReference = _poolableObjectQueue[i];
            PoolableObjectList[i] = data;
                
            for (int a = 0; a < data.InitialPoolAmount; a++)
            {
                var obj = Instantiate(data.Prefab, transform, true);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }

        // foreach (PoolableObjectData data in _poolableObjectList) {
        //     Queue<GameObject> queue = new Queue<GameObject>();
        //     data.QueueReference = PoolableObjectList.Add(queue);
                
        //     for (int i = 0; i < data.InitialPoolAmount; i++)
        //     {
        //         var obj = Instantiate(data.Prefab, transform, true);
        //         obj.SetActive(false);
        //         queue.Enqueue(obj);
        //     }
        // }
    }

    public void EnqueueObject(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;

        obj.gameObject.SetActive(false);

        foreach (PoolableObjectData data in PoolableObjectList) {
            if (data.Prefab == obj) {
                Debug.Log("Object queued " + obj);
                data.QueueReference.Enqueue(obj);
            }
        }
    }

    public GameObject DequeuePoolableGameObject(PoolableObjectData data)
    {
        // If there are no remaining poolable objects, initialize more.
        if (data.QueueReference.Count <= 0) {
            var obj = Instantiate(data.Prefab, transform, true);
            obj.SetActive(false);
            data.QueueReference.Enqueue(obj);
        }

        var deQueuedPoolObject = data.QueueReference.Dequeue();
        if (deQueuedPoolObject.activeSelf) DequeuePoolableGameObject(data);
        deQueuedPoolObject.SetActive(true);
        return deQueuedPoolObject;
    }
    
}
