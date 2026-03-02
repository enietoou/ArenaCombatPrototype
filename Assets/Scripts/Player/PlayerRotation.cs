using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotation : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody _rb;
    [SerializeField] private float rotationSpeed = 10f;

    private Quaternion _targetRotation;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _targetRotation = _rb.rotation;
    }

    private void Update()
    {
        CalculateRotation();
    }

    private void FixedUpdate()
    {
        Quaternion smoothRotation = Quaternion.Slerp(
            _rb.rotation,
            _targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        _rb.MoveRotation(smoothRotation);
    }

    private void CalculateRotation()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 direction = point - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.0001f)
            {
                direction.Normalize();
                _targetRotation = Quaternion.LookRotation(direction);
            }
        }
    }
}