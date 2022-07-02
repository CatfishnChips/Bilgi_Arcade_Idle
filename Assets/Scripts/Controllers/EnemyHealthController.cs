using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    private int _maxHealth;
    //private float _invulnerableTime;
    private float _currentHealth;

    private EnemyManager _enemyManager;

    private void Awake() 
    {
        _enemyManager = GetComponent<EnemyManager>();
    }

    private void OnEnable() 
    {
        ResetVariables();
    }

    public void HandleDamage(float amount) {
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            _enemyManager.HandleDeath();
        }
    }

    private void ResetVariables() {
        _currentHealth = _maxHealth;
    }
    
    public void SetVaribles(int health) => _maxHealth = health;
}
