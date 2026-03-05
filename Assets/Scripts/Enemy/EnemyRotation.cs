using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyTargetSystem))]
public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    
    // private float _rotationSpeed = 1f;
    
    private Rigidbody _rb;
    private EnemyTargetSystem _targetSystem;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetSystem = GetComponent<EnemyTargetSystem>();
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
            stats.rotationSpeed * Time.fixedDeltaTime);
        
        _rb.MoveRotation(smoothRotation);
    }
}
