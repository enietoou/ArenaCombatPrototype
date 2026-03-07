using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyTargetSystem))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float separationRadius = 1.2f;
    [SerializeField] private float separationStrength = 2f;

    private Enemy _enemy;

    private EnemyTargetSystem _targetSystem;
    private Rigidbody _rb;
    private EnemyRotation _rotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetSystem = GetComponent<EnemyTargetSystem>();
        _enemy = GetComponent<Enemy>();
        _rotation = GetComponent<EnemyRotation>();
    }

    public void MoveTowards(Vector3 direction, bool lookingAtTarget = true)
    {
        Vector3 separation = CalculateSeparation();

        direction += separation;
        
        direction.y = 0;
        
        if (lookingAtTarget && direction.magnitude <= _enemy.Stats.stoppingDistance) return;
        if (!lookingAtTarget && direction.magnitude <= 0.1f) return;
        
        _rotation.SetDirection(direction);

        Vector3 forward = transform.forward;

        Vector3 newPosition = _rb.position + forward * (_enemy.Stats.moveSpeed * Time.fixedDeltaTime);
        
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
