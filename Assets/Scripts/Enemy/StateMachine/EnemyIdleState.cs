
public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine sm) : base(sm) {}

    public override void Update()
    {
        var targetSystem = StateMachine.GetComponent<EnemyTargetSystem>();

        if (targetSystem.CurrentTarget != null)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
        }
    }
}
