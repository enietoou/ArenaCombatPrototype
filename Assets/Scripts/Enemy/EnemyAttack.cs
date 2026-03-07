using System;
using Combat.Interfaces;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;

    private float _nextAttackTime;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public bool CanAttack()
    {
        return Time.time >= _nextAttackTime;
    }

    public void Attack(ITargetable target)
    {
        if (!CanAttack()) return;

        _nextAttackTime = Time.time + _enemy.Stats.attackRate;

        var transformTarget = target.GetTransform();
        var damageable = transformTarget.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_enemy.Stats.damage);
        }
    }
}