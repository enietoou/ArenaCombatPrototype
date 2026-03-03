using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private float range = 20f;
    [SerializeField] private Transform firePoint;

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
        Vector3 direction = firePoint.forward;
        direction.y = 0f;
        direction.Normalize();

        Ray ray = new Ray(firePoint.position, direction);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.5f);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
