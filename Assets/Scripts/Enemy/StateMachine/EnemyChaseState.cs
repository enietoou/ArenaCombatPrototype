using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private EnemyTargetSystem _targetSystem;
    private EnemyMovement _movement;
    
    public EnemyChaseState(EnemyStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        _targetSystem = StateMachine.GetComponent<EnemyTargetSystem>();
        _movement = StateMachine.GetComponent<EnemyMovement>();
    }

    public override void FixedUpdate()
    {
        var target = _targetSystem.CurrentTarget;

        if (target == null)
        {
            StateMachine.ChangeState(StateMachine.SearchState);
            return;
        }
        
        Vector3 dir = target.GetTransform().position - StateMachine.transform.position;
        _movement.MoveTowards(dir);
    }
}
