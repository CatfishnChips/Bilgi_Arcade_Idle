using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnerLocations;
    private List<Vector3> _spawnerPoints;


    // Start is called before the first frame update
    void Start()
    {
        SetupSpawnerList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupSpawnerList() 
    {
        foreach (Transform transform in _spawnerLocations.GetComponentInChildren<Transform>()) {
            _spawnerPoints.Add(transform.position);
        }
    }
}
