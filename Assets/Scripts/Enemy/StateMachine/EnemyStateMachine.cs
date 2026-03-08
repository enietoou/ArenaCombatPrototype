using System;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState _currentState;
    private EnemyState _previousState;

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemySearchState SearchState { get; private set; }
    public EnemyStunState StunState { get; private set; }
    public bool IsStunned => _currentState is EnemyStunState;

    private EnemyHealth _health;

    private void Awake()
    {
        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        SearchState = new EnemySearchState(this);
        StunState = new EnemyStunState(this);

        _health = GetComponent<EnemyHealth>();

        if (_health != null)
        {
            _health.OnDamage += HandleDamage;
        }
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
        if (_currentState != null)
        {
            _currentState.Exit();
            if (!(_currentState is EnemyStunState))
            {
                _previousState = _currentState;
            }
        }
        
        _currentState = newState;
        _currentState?.Enter();
    }

    public void ReturnToPreviousState()
    {
        if (_previousState == null)
        {
            ChangeState(IdleState);
            return;
        };
        
        ChangeState(_previousState);
    }

    private void HandleDamage()
    {
        if (IsStunned)
        {
            StunState.Refresh();
        }
        else
        {
            ChangeState(StunState);
        }
    }

    private void OnDestroy()
    {
        _health.OnDamage -= HandleDamage;
    }
    
    public void ResetState()
    {
        _currentState?.Exit();
        _previousState = null;
        
        ChangeState(IdleState);
    }
}
