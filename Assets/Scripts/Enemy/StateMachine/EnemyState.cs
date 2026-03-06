
public abstract class EnemyState
{
    protected EnemyStateMachine StateMachine;

    public EnemyState(EnemyStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}
