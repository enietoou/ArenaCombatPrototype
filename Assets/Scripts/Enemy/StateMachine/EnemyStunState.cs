using UnityEngine;

public class EnemyStunState : EnemyState
{
    private float _stunTime = 0.3f;
    private float _timer;
    
    public EnemyStunState(EnemyStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        _timer = _stunTime;
    }

    public override void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer <= 0)
        {
            StateMachine.ReturnToPreviousState();
        }
    }

    public void Refresh()
    {
        _timer = _stunTime;
    }
}
