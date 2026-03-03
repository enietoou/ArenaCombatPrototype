using UnityEngine;

namespace Combat.Interfaces
{
    public interface ITargetable : IDamageable
    {
        bool IsAlive { get;  }
        Transform GetTransform();
    }
}