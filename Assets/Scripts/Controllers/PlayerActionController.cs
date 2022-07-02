using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] private float _attackTargetingRange;
    [SerializeField] private LayerMask _attackRaycastLayerMask;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackKnockback;
    [SerializeField] private float _attackStunDuration;
    [SerializeField] private float _attackCooldown;
    [SerializeField] [Range(0, 180)] private float _attackAngle;
    [SerializeField] private ThrowableBarrel _barrelReference;
    [SerializeField] private GameObject _bulletReference;
    [SerializeField] private Transform _indicatorParent;
    [SerializeField] private ParticleSystem _attackIndicator, _abilityIndicator1, _abilityIndicator2, _abilityIndicator3;
    private List<RaycastHit> _rayCastHitList = new List<RaycastHit>();
    private Vector3 _attackDirection, _ability1Direction, _ability2Direction, _ability3Direction;
    private float _currentAttackCooldown;
    private bool IsAttackReady {
        get {
            if (_currentAttackCooldown <= 0) return true;
            else return false;
        }
    }

    private void Awake() {
        _barrelReference = FindObjectOfType<ThrowableBarrel>();
    }

    private void Update() {
        if (_currentAttackCooldown > 0) {
            _currentAttackCooldown -= Time.deltaTime;
        }
    }

    public void AimAttack(JoystickInputParams inputParams) {
        _attackDirection = new Vector3 (inputParams.DirectionInputValue.x, 0f, inputParams.DirectionInputValue.y);
        _indicatorParent.forward = _attackDirection;
        _attackIndicator.Play();
    }

    public void ExecuteAttack() {
        if (!IsAttackReady) return;
        _currentAttackCooldown = _attackCooldown;

        if (_attackDirection == Vector3.zero) {
            Debug.Log("Quick Action");
            AttackClosestEnemy();
        }
        else {
            Debug.Log("Aimed Action");
            AttackUsingDirection();
        }
        _attackDirection = Vector3.zero;
        _rayCastHitList.Clear();
    }

    private void AttackUsingDirection() {
        if (_rayCastHitList.Count <= 0) {
            RaycastHit[] _raycastHits = Physics.SphereCastAll(transform.position, _attackTargetingRange, Vector3.up, _attackTargetingRange, _attackRaycastLayerMask);
            _rayCastHitList.AddRange(_raycastHits);
        }

        foreach (RaycastHit hit in _rayCastHitList) {
            Vector3 relativePoint = hit.collider.transform.position - transform.position;
            relativePoint.y = 0;
            Vector3 relativeDirection = relativePoint / relativePoint.magnitude;

            float angle = Vector3.Angle(_attackDirection, relativeDirection);
            

            if (angle <= _attackAngle) {            
                hit.collider.transform.GetComponentInParent<EnemyManager>().HandleHit(_attackDamage, _attackKnockback, relativeDirection, _attackStunDuration);
            }
        }
    }

    private Vector3 FindClosestEnemyDirection() {
         RaycastHit[] _raycastHits = Physics.SphereCastAll(transform.position, _attackTargetingRange, Vector3.up, _attackTargetingRange, _attackRaycastLayerMask);
        _rayCastHitList.AddRange(_raycastHits);

        if (_raycastHits.Length <= 0) return Vector3.zero;

        bool firstIteration = true;
        float closestDistance = 0;
        Vector3 closestPosition = Vector3.zero;

        foreach (RaycastHit hit in _raycastHits) {
            
            if (firstIteration == true) {
                firstIteration = false;
                closestDistance = hit.distance;
                closestPosition = hit.collider.transform.position;
            }

            if (hit.distance <= closestDistance) {
                closestDistance = hit.distance;
                closestPosition = hit.collider.transform.position;
            }  
        }
        Vector3 relativePoint = closestPosition - transform.position;
        relativePoint.y = 0;
        Vector3 relativeDirection = relativePoint / relativePoint.magnitude;
        return relativeDirection;
    }

    private void AttackClosestEnemy() {
        _attackDirection = FindClosestEnemyDirection();
        if (_attackDirection == Vector3.zero) return;
        AttackUsingDirection();
    }

    private void Ability1ClosestEnemy() {
        _ability1Direction = FindClosestEnemyDirection();
        if (_ability1Direction == Vector3.zero) return;
        Ability1UsingDirection();
    }

    public void AimAbility1(JoystickInputParams inputParams) {
        _ability1Direction = new Vector3 (inputParams.DirectionInputValue.x, 0f, inputParams.DirectionInputValue.y);
        _indicatorParent.forward = _ability1Direction;
        _abilityIndicator1.Play();
    }

     private void Ability1UsingDirection() {

    }

    public void ExecuteAbility1() {
        
    }

    #region Ability #2

    private void Ability2ClosestEnemy() {
        _ability2Direction = FindClosestEnemyDirection();
        if (_ability2Direction == Vector3.zero) return;
        Ability2UsingDirection();
    }

    public void AimAbility2(JoystickInputParams inputParams) {
        _ability2Direction = new Vector3 (inputParams.DirectionInputValue.x, 0f, inputParams.DirectionInputValue.y);
        _indicatorParent.forward = _ability2Direction;
        _abilityIndicator2.Play();
    }

    private void Ability2UsingDirection() {
        //Vector3 spawnPosition = new Vector3(transform.position.x * transform.forward.x, transform.position.y, transform.position.z * transform.forward.z);
        _barrelReference.ThrowBarrel(transform.position, _ability2Direction);
    }

    public void ExecuteAbility2() {
        if (_barrelReference._isActive) return;

        if (_ability2Direction == Vector3.zero) {
            Debug.Log("Quick Action");
            Ability2ClosestEnemy();
        }
        else {
            Debug.Log("Aimed Action");
            Ability2UsingDirection();
        }
        _ability2Direction = Vector3.zero;
        _rayCastHitList.Clear();   
    }
    #endregion

    private void Ability3ClosestEnemy() {
        _ability3Direction = FindClosestEnemyDirection();
        if (_ability3Direction == Vector3.zero) return;
        Ability3UsingDirection();
    }

    public void AimAbility3(JoystickInputParams inputParams) {
        _ability3Direction = new Vector3 (inputParams.DirectionInputValue.x, 0f, inputParams.DirectionInputValue.y);
        _indicatorParent.forward = _ability3Direction;
        _abilityIndicator3.Play();
    }

    private void Ability3UsingDirection() {

    }

    public void ExecuteAbility3() {

    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, _attackDirection);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _attackTargetingRange);
    }
}
