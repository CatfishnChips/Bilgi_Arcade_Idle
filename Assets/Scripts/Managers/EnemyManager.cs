using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _meshList;
    [SerializeField] private EnemyTypes _enemyType;
    [HideInInspector] public EnemyTypes EnemyType {
        get => _enemyType;

        set {
            _enemyType = value;
            for (int i = 0; i < _meshList.Count; i++)
            {
                _meshList[i].SetActive(false);
            }
            _meshList[(int)value].SetActive(true);

        }
    }

    private EnemyMovementController _movementController;
    private EnemyAnimationController _animationController;

    private void Awake() 
    {
        _movementController = GetComponent<EnemyMovementController>();
        _animationController = GetComponent<EnemyAnimationController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupMesh() {
        EnemyType = _enemyType;
    }
}
