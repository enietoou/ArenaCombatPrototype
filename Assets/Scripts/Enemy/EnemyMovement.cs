using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyTargetSystem))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1f;

    private EnemyTargetSystem _targetSystem;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetSystem = GetComponent<EnemyTargetSystem>();
    }

    private void FixedUpdate()
    {
        var target = _targetSystem.CurrentTarget;
        if (target == null) return;
        
        Vector3 direction = target.GetTransform().position - transform.position;

        MoveTowardsPlayer(direction);
    }

    private void MoveTowardsPlayer(Vector3 direction)
    {
        direction.y = 0;
        
        if (direction.magnitude <= stoppingDistance) return;
        
        direction.Normalize();
        
        Vector3 newPosition = _rb.position + direction * (moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        _rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
