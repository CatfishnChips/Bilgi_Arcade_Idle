using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public EnemyStates _state;
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
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _poolingTimer;
    [SerializeField] private float _searchRange;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _stoppingDistance;

    private EnemyHealthController _healthController;
    private EnemyMovementController _movementController;
    private EnemyAnimationController _animationController;
    private EnemyPhysicsController _physicsController;
    private PoolableObjectController _poolingController;
    private WaveManager _waveManager;

    private void Awake() 
    {   
        _healthController = GetComponent<EnemyHealthController>();
        _movementController = GetComponent<EnemyMovementController>();
        _animationController = GetComponent<EnemyAnimationController>();
        _physicsController = GetComponentInChildren<EnemyPhysicsController>();
        _poolingController = GetComponent<PoolableObjectController>();
        _waveManager = FindObjectOfType<WaveManager>();
        SetupControllerVariables();
    }

    private void OnEnable() {
        ResetState();
    }

    private void OnDisable() {
        HandleDeath();
    }

    void Start()
    {
        SetupMesh();
    }

    void Update()
    {
        
    }

    private void SetupControllerVariables() {
        _healthController.SetVaribles(_maxHealth);
        _movementController.SetVariables(_searchRange, _followSpeed, _stoppingDistance);
    }

    private void SetupMesh() {
        EnemyType = _enemyType;
    }

    public void HandleDeath() {
       _poolingController.EnqueueCheck(_poolingTimer);
       _waveManager.RemoveFromSpawnedList(gameObject);
       _state = EnemyStates.StopAI;
    }
    private void ResetState() {
        _state = EnemyStates.Idle;
    }

    public void HandleHit(float damage, float knockback, Vector3 direction, float duration) {
        _state = EnemyStates.Stunned;
        _physicsController.HandleKnockback(knockback, direction);
        _healthController.HandleDamage(damage);
        Invoke("ResetState", duration);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _searchRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stoppingDistance);
    }
}
