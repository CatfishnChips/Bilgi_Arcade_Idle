using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Enums;

public class EnemyMovementController : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private NavMeshAgent _navMesh;
    private float _searchRange;
    private float _followSpeed;
    private float _stoppingDistance;
    private PlayerManager _playerManager;
    private Vector3 _targetPosition;
    //private float _maxFollowDistance;
    private Vector3 _spawnPosition;

    private void Awake() 
    {   
        _enemyManager = GetComponent<EnemyManager>();
        _navMesh = GetComponent<NavMeshAgent>();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnDisable() {
        _targetPosition = transform.position;
    }

    void Start()
    {
        _navMesh.stoppingDistance = _stoppingDistance;
        _navMesh.speed = _followSpeed;
    }

    void Update()
    {
        if (_enemyManager._state == EnemyStates.StopAI) return;

        SearchTarget();
        FollowTarget();
 
        if (_enemyManager._state == EnemyStates.Stunned) {
            if (!_navMesh.isStopped) {
                _navMesh.velocity = Vector3.zero;
                _navMesh.isStopped = true;
            }
        }
        else {
            if (_navMesh.isStopped) _navMesh.isStopped = false; 
        }
    }

    private void SearchTarget() {
        if (_playerManager == null) return;
        if (Vector3.Distance(transform.position, _playerManager.transform.position) <= _searchRange) {
            _targetPosition = _playerManager.transform.position;
        }
        else _targetPosition = transform.position;
    }

    private void FollowTarget() {
        _navMesh.SetDestination(_targetPosition);
    }

    public void SetVariables(float range, float speed, float distance) { _searchRange = range; _followSpeed = speed;  _stoppingDistance = distance; }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _searchRange);
    }
}
