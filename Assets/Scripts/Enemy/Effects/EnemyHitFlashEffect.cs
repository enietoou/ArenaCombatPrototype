using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyHitFlashEffect : MonoBehaviour
{
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float flashTime = 0.1f;

    private Color _originalColor;
    private EnemyHealth _health;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();

        if (enemyRenderer != null)
        {
            _originalColor = enemyRenderer.material.color;
        }
    }

    private void OnEnable()
    {
        _health.OnDamage += HandleDamage;
    }

    private void OnDisable()
    {
        _health.OnDamage -= HandleDamage;
    }

    private void HandleDamage()
    {
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        enemyRenderer.material.color = damageColor;
        
        yield return new WaitForSeconds(flashTime);
        
        enemyRenderer.material.color = _originalColor;
    }
}
