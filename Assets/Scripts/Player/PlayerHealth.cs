using System;
using Combat.Interfaces;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, ITargetable
{
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;
    public event Action<int> OnHealthChange;
    
    public bool IsAlive => _currentHealth > 0;
    
    public Transform GetTransform() => transform;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        
        OnHealthChange?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Debug.Log("Player is dead");
        }
    }

    private void OnEnable()
    {
        TargetRegistry.AllTargets.Add(this);
    }

    private void OnDisable()
    {
        TargetRegistry.AllTargets.Remove(this);
    }
}
