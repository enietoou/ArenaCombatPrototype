using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyRotation : MonoBehaviour
{
    private Enemy _enemy;
    
    private Rigidbody _rb;
    
    private Vector3 _desiredDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _enemy = GetComponent<Enemy>();
    }

    public void SetDirection(Vector3 direction)
    {
        direction.y = 0;

        if (direction.sqrMagnitude < 0.001f) return;
        
        _desiredDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (_desiredDirection.sqrMagnitude < 0.001f) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(_desiredDirection);

        Quaternion smoothRotation = Quaternion.RotateTowards(
            _rb.rotation,
            targetRotation,
            _enemy.Stats.rotationSpeed * Time.fixedDeltaTime * 360f);
        
        _rb.MoveRotation(smoothRotation);
    }
}
