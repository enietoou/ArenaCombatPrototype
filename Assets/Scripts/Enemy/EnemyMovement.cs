using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Transform _target;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
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
        direction.Normalize();
        
        Vector3 newPosition = _rb.position + direction * (moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
}
