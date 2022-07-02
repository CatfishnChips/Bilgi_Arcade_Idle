using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicsController : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    private void Awake() 
    {
        _enemyManager = GetComponentInParent<EnemyManager>();
        _collider = GetComponent<Collider>();
    }

    private void OnColliderEnter(Collider collider) 
    {
       // if (collider.tag = "Attack");
    }

    public void HandleKnockback(float amount, Vector3 direction) {
        _rigidbody.AddForce(new Vector3(direction.x, 2f, direction.z) * amount, ForceMode.Impulse);
    }
}
