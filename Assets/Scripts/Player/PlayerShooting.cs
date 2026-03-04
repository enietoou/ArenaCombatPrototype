using Combat.Interfaces;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private float range = 20f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource hitSound;

    private float _nextFireTime;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, firePoint.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);

            Vector3 direction = point - firePoint.position;
            direction.y = 0f;
            direction.Normalize();

            Ray shootRay = new Ray(firePoint.position, direction);

            Debug.DrawRay(shootRay.origin, shootRay.direction * range, Color.red, 0.5f);

            if (Physics.Raycast(shootRay, out RaycastHit hit, range))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    hitSound.Play();
                }

                EnemyMovement enemyMove = hit.collider.GetComponent<EnemyMovement>();

                if (enemyMove != null)
                {
                    enemyMove.ApplyKnockback(direction, 10f);
                }
            }
        }
    }
}
