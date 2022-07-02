using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _invulnerableTime;
    private float _currentHealth;

    private PlayerManager _playerManager;

    private void Awake() 
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable() 
    {
        ResetVariables();
    }

    public void HandleDamage(float amount) {
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            _playerManager.HandleDeath();
        }
    }

    private void ResetVariables() {
        _currentHealth = _maxHealth;
    }
}
