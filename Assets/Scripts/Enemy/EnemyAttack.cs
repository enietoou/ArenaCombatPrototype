using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private float attackDistance = 1.5f;

    private float _nextAttackTime;
    private Transform _target;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_target == null) return;
        
        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= attackDistance && Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + attackRate;
            Attack();
        }
    }

    private void Attack()
    {
        IDamageable damageable = _target.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
