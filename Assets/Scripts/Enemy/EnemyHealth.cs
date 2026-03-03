using Combat.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
