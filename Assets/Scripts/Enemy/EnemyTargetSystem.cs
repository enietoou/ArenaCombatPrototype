using Combat.Interfaces;
using UnityEngine;

public class EnemyTargetSystem : MonoBehaviour
{
    private ITargetable _currentTarget;
    
    public ITargetable CurrentTarget => _currentTarget;

    private void Update()
    {
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
        }
    }

    private void FindNewTarget()
    {
        float closestDistance = Mathf.Infinity;
        ITargetable closest = null;

        foreach (var t in TargetRegistry.AllTargets)
        {
            if (t.IsAlive)
            {
                float dist = Vector3.Distance(
                    transform.position,
                    t.GetTransform().position
                );

                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closest = t;
                }
            }
        }

        _currentTarget = closest;
    }
}
