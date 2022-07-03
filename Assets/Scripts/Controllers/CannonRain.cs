using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRain : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public bool _isActive;

    #endregion

    #region Serialized Variables

    [SerializeField] private List<EnemyManager> _enemiesInsideArea;
    [SerializeField] private int _damagePerHit;
    [SerializeField] private float _hitInterval;
    [SerializeField] private float _duration;
    [SerializeField] private float _radius;

    #endregion

    #region Private Variables
    
    private SphereCollider _trigger;
    private float _currentDuration;
    private float _currentInterval;

    #endregion

    #endregion

    private void Awake() {
        _trigger = GetComponent<SphereCollider>();
    }

    void Start()
    {
        _isActive = false;
        _currentDuration = 0;
        _currentInterval = _hitInterval;
        _trigger.radius = _radius;
        _trigger.enabled = false;
    }

    void Update()
    {
        if (_isActive) {
            if (_currentInterval > 0) {
                _currentInterval -= Time.deltaTime;
            }
            else {
                HandleDamage();
                _currentInterval = _hitInterval;
            }

            if (_currentDuration > 0) {
                _currentDuration -= Time.deltaTime;
            }
            else {
                _isActive = false;
            }
        }
    }

    private void HandleDamage() {
        foreach (EnemyManager enemy in _enemiesInsideArea) {
            enemy.HandleHit(_damagePerHit, 0f, Vector3.zero, 0.1f);
        }
    }

    public void SpawnObject(Vector3 position) {
        if (_isActive) return;

        _isActive = true;
        transform.position = position;
        _currentDuration = _duration;
        _currentInterval = _hitInterval;
        _trigger.enabled = true;
    }

    private void DespawnObject() {
        _isActive = false;
        _enemiesInsideArea.Clear();
        _trigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            _enemiesInsideArea.Add(other.gameObject.GetComponentInParent<EnemyManager>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy") {
            _enemiesInsideArea.Remove(other.gameObject.GetComponentInParent<EnemyManager>());
        }
    }
}
