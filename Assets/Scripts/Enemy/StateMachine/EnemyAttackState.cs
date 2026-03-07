using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private EnemyTargetSystem _targetSystem;
    private EnemyAttack _attack;
    private Enemy _enemy;

    public EnemyAttackState(EnemyStateMachine sm) : base(sm) {}

    public override void Enter()
    {
        _targetSystem = StateMachine.GetComponent<EnemyTargetSystem>();
        _attack = StateMachine.GetComponent<EnemyAttack>();
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

        if (distance > _enemy.Stats.attackDistance)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
            return;
        }

        _attack.Attack(target);
    }
}