using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1f;
    private float _sqrStoppingDistance;

    private Transform _target;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _sqrStoppingDistance = stoppingDistance * stoppingDistance;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = _target.position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude <= _sqrStoppingDistance) return;
        
        direction.Normalize();
        
        Vector3 newPosition = _rb.position + direction * (moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
}
