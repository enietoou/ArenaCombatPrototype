using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyTargetSystem))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1.5f;
    private float _sqrStoppingDistance;

    private EnemyTargetSystem _targetSystem;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetSystem = GetComponent<EnemyTargetSystem>();
        _sqrStoppingDistance = stoppingDistance * stoppingDistance;
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
        if (direction.sqrMagnitude <= _sqrStoppingDistance) return;
        direction.y = 0;
        
        direction.Normalize();
        
        Vector3 newPosition = _rb.position + direction * (moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
}
