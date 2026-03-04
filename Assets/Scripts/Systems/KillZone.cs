using System;
using UnityEngine;
using Combat.Interfaces;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(9999999);
        }
    }
}
