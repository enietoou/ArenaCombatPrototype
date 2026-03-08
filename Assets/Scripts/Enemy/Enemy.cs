using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyRotation))]
[RequireComponent(typeof(EnemyTargetSystem))]
[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAttack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    
    public EnemyStats Stats => stats;

    private EnemyHealth _health;
    private EnemyStateMachine _stateMachine;
    private EnemyMovement _movement;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _stateMachine = GetComponent<EnemyStateMachine>();
        _movement = GetComponent<EnemyMovement>();
    }

    public void ResetEnemy()
    {
        _health.ResetHealth();

        _stateMachine.ResetState();

        _movement.ResetMovement();
    }
}
