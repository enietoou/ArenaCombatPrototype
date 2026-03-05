using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyTargetSystem))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    
    // private float _moveSpeed = 3f;
    // private float _stoppingDistance = 1f;
    [SerializeField] private float separationRadius = 1.2f;
    [SerializeField] private float separationStrength = 2f;

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
        Vector3 separation = CalculateSeparation();

        direction += separation;
        
        direction.y = 0;
        
        if (direction.magnitude <= stats.stoppingDistance) return;
        
        direction.Normalize();
        
        Vector3 newPosition = _rb.position + direction * (stats.moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        _rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private Vector3 CalculateSeparation()
    {
        Collider[] neighbors = Physics.OverlapSphere(transform.position, separationRadius);

        Vector3 separation = Vector3.zero;

        foreach (var col in neighbors)
        {
         if (col.gameObject == gameObject) continue;

         if (!col.CompareTag("Enemy")) continue;
         
         Vector3 away = transform.position - col.transform.position;

         separation += away.normalized / away.magnitude;
        }

        return separation * separationStrength;
    }
}
