using UnityEngine;

public class EnemySearchState : EnemyState
{
    private Vector3 _lastKnownPosition;
    private EnemyMovement _movement;
    private EnemyTargetSystem _targetSystem;
    
    public EnemySearchState(EnemyStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        _targetSystem = StateMachine.GetComponent<EnemyTargetSystem>();
        _movement = StateMachine.GetComponent<EnemyMovement>();
        _lastKnownPosition = _targetSystem.LastKnownPosition;
    }

    public override void FixedUpdate()
    {
        if (_targetSystem.CurrentTarget != null)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
        }
        
        Vector3 dir = _lastKnownPosition - StateMachine.transform.position;
        
        _movement.MoveTowards(dir, false);

        if (dir.magnitude < 1f)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
