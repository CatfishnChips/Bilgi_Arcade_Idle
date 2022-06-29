using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class ThrowableBarrel : MonoBehaviour
{
    public bool _isActive;
    [SerializeField] private ThrowableBarrelStates _state;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private float _timer;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _raycastLayerMask;
    [SerializeField] private float _explosionDamage;
    private float _distanceTravelled;
    private Vector3 _startingPosition;
    private float _countdownTime;
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;
    private float Speed {
        get {
            if (_state == ThrowableBarrelStates.Moving) return _speed;
            else return 0;
        }
    }

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        if (!_isActive) return;

        HandleMovement();
        switch (_state) {

            case ThrowableBarrelStates.Moving: 
            CheckDistance();
            break;

            case ThrowableBarrelStates.Countdown:
            HandleCountdown();
            break;
        }
    }

    private void HandleMovement() {
        _rigidbody.velocity = Speed * transform.forward * Time.deltaTime;
    }

    private void CheckDistance() {
        _distanceTravelled = Vector3.Distance(transform.position, _startingPosition);
        if (_distanceTravelled >= _distance) {
            _state = ThrowableBarrelStates.Countdown;
        }
    }

    private void HandleCountdown() {
        _countdownTime += Time.deltaTime;
        if (_countdownTime >= _timer) {
            _state = ThrowableBarrelStates.Idle;
            HandleExplosion();
        }
    }

    private void HandleExplosion() {
        //_animator
        foreach (RaycastHit collision in Physics.SphereCastAll(transform.position, _explosionRadius, Vector3.up, _explosionRadius, _raycastLayerMask)) {
            var enemyManager = collision.collider.transform.parent.GetComponent<EnemyManager>();
            //enemyManager.HandleDamage(_explosionDamage);
        }
        _meshRenderer.enabled = false;
        _boxCollider.enabled = false;
        _isActive = false;
    }

    private void ResetValues() {
        _direction = Vector3.zero;
        _countdownTime = 0;
        _startingPosition = Vector3.zero;
        _distanceTravelled = 0;
        _meshRenderer.enabled = true;
        _boxCollider.enabled = true;
        _isActive = true;
    }

    public void ThrowBarrel(Vector3 direction) {
        ResetValues();
        _state = ThrowableBarrelStates.Moving;
        _direction = direction;
        transform.forward = _direction;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.parent.tag == "Player") return;
        if (collision.transform.parent.tag == "Enemy") return;

        if (collision.transform.parent.tag == "Attack") {
            _state = ThrowableBarrelStates.Idle;
            HandleExplosion();
        }
        else {
            _state = ThrowableBarrelStates.Countdown;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
