using Combat.Interfaces;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public EnemyStats stats;

    private float _nextAttackTime;

    public bool CanAttack()
    {
        return Time.time >= _nextAttackTime;
    }

    public void Attack(ITargetable target)
    {
        if (!CanAttack()) return;

        _nextAttackTime = Time.time + stats.attackRate;

        var transformTarget = target.GetTransform();
        var damageable = transformTarget.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(stats.damage);
        }
    }
}