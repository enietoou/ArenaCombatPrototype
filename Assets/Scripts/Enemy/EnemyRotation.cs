using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyRotation : MonoBehaviour
{
    private Enemy _enemy;
    
    private Rigidbody _rb;
    private EnemyTargetSystem _targetSystem;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetSystem = GetComponent<EnemyTargetSystem>();
        _enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        RotateToTarget();
    }

    private void RotateToTarget()
    {
        var target = _targetSystem.CurrentTarget;
        
        if (target == null) return;
        
        Vector3 direction = target.GetTransform().position - _rb.position;

        direction.y = 0f;
        
        if (direction.sqrMagnitude < 0.001f) return;
        
        direction.Normalize();
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        Quaternion smoothRotation = Quaternion.Slerp(
            _rb.rotation, 
            targetRotation, 
            _enemy.Stats.rotationSpeed * Time.fixedDeltaTime);
        
        _rb.MoveRotation(smoothRotation);
    }
}
