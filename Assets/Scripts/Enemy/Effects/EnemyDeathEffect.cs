using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyDeathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private AudioClip deathSound;

    private EnemyHealth _health;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        _health.OnDeath += PlayEffect;
    }

    private void OnDisable()
    {
        _health.OnDeath -= PlayEffect;
    }

    private void PlayEffect(EnemyHealth enemy)
    {
        if (deathParticles != null)
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }
}
