using Combat.Interfaces;
using UnityEngine;

[RequireComponent(typeof(EnemyTargetSystem))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    
    // private int _damage = 10;
    // private float _attackRate = 1f;
    // private float _attackDistance = 1.5f;

    private float _nextAttackTime;
    private EnemyTargetSystem _targetSystem;

    private void Awake()
    {
        _targetSystem = GetComponent<EnemyTargetSystem>();
    }

    private void Update()
    {
        var target = _targetSystem.CurrentTarget;
        if (target == null) return;

        float distance = Vector3.Distance(
            transform.position,
            target.GetTransform().position
        );

        if (distance <= stats.attackDistance && Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + stats.attackRate;
            Attack(target);
        }
    }

    private void Attack(ITargetable target)
    {
        var transformTarget = target.GetTransform();
        var damageable = transformTarget.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(stats.damage);
        }
    }
}