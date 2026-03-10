using System;
using System.Collections;
using Combat.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Enemy _enemy;
    private int _currentHealth;
    
    public event Action OnDamage;
    public event Action<EnemyHealth> OnDeath;
    

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _currentHealth = _enemy.Stats.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        
        OnDamage?.Invoke();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);

        if (_enemy.Stats.healthDropPrefab != null && Random.value < _enemy.Stats.dropChance)
        {
            Instantiate(_enemy.Stats.healthDropPrefab, transform.position, Quaternion.identity);
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _enemy.Stats.maxHealth;
        gameObject.SetActive(true);
    }
}
