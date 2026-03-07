using System;
using System.Collections;
using Combat.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float flashTime = 0.1f;

    private Enemy _enemy;

    private Color _originalColor;
    
    private int _currentHealth;
    
    public event Action<EnemyHealth> OnDeath;
    

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _currentHealth = _enemy.Stats.maxHealth;
        _originalColor = enemyRenderer.material.color;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        StartCoroutine(DamageFlash());

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator DamageFlash()
    {
        enemyRenderer.material.color = damageColor;
        
        yield return new WaitForSeconds(flashTime);

        enemyRenderer.material.color = _originalColor;
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
