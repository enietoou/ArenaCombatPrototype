using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyRotation))]
[RequireComponent(typeof(EnemyTargetSystem))]
[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAttack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    
    public EnemyStats Stats => stats;
}
