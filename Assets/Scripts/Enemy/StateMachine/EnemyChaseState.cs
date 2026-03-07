using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private EnemyTargetSystem _targetSystem;
    private EnemyMovement _movement;
    private Enemy _enemy;
    
    public EnemyChaseState(EnemyStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        _targetSystem = StateMachine.GetComponent<EnemyTargetSystem>();
        _movement = StateMachine.GetComponent<EnemyMovement>();
        _enemy = StateMachine.GetComponent<Enemy>();
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

        float distance = dir.magnitude;

        if (distance <= _enemy.Stats.attackDistance)
        {
            StateMachine.ChangeState(StateMachine.AttackState);
            return;
        }
        
        _movement.MoveTowards(dir);
    }
}
