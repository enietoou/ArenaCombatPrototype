using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private EnemyTargetSystem _targetSystem;
    private EnemyAttack _attack;
    private EnemyStats _stats;

    public EnemyAttackState(EnemyStateMachine sm) : base(sm) {}

    public override void Enter()
    {
        _targetSystem = StateMachine.GetComponent<EnemyTargetSystem>();
        _attack = StateMachine.GetComponent<EnemyAttack>();
        _stats = StateMachine.GetComponent<EnemyAttack>().stats;
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

        if (distance > _stats.attackDistance)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
            return;
        }

        _attack.Attack(target);
    }
}