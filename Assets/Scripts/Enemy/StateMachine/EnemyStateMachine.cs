using System;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState _currentState;

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemySearchState SearchState { get; private set; }

    private void Awake()
    {
        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        SearchState = new EnemySearchState(this);
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    private void Update()
    {
        _currentState?.Update();
    }
    
    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public void ChangeState(EnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
