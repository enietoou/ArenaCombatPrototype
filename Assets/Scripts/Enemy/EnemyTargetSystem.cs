using Combat.Interfaces;
using UnityEngine;

public class EnemyTargetSystem : MonoBehaviour
{
    [SerializeField] private LayerMask visionMask;

    private Enemy _enemy;

    private float _nextUpdateTime;
    
    private ITargetable _currentTarget;
    
    public ITargetable CurrentTarget => _currentTarget;
    
    public Vector3 LastKnownPosition { get; private set; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Time.time < _nextUpdateTime) return;

        _nextUpdateTime = Time.time + 0.2f;
        
        ValidateTarget();

        if (_currentTarget == null)
        {
            FindNewTarget();
        }
    }

    private void ValidateTarget()
    {
        if (_currentTarget == null) return;

        if (!_currentTarget.IsAlive)
        {
            _currentTarget = null;
            return;
        }

        if (_currentTarget != null)
        {
            LastKnownPosition = _currentTarget.GetTransform().position;
        }

        if (!HasLineOfSight(_currentTarget))
        {
            _currentTarget = null;
        }
    }

    private void FindNewTarget()
    {
        float closestDistance = Mathf.Infinity;
        ITargetable closest = null;

        foreach (var t in TargetRegistry.AllTargets)
        {
            if (t == null) continue;
            if (!t.IsAlive) continue;

            Vector3 dir = t.GetTransform().position - transform.position;
            float dist = dir.magnitude;

            if (dist > _enemy.Stats.aggroRadius) continue;

            if (!HasLineOfSight(t)) continue;

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = t;
            }
        }

        _currentTarget = closest;
    }
    
    private bool HasLineOfSight(ITargetable target)
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 targetPos = target.GetTransform().position + Vector3.up * 0.5f;

        Vector3 direction = (targetPos - origin).normalized;
        float distance = Vector3.Distance(origin, targetPos);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance, visionMask))
        {
            return hit.collider.GetComponent<ITargetable>() != null;
        }

        return false;
    }
}
