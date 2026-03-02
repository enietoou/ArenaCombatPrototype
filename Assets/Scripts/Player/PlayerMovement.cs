using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody _rb;
    private Vector3 _movementInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetMovementInput(Vector2 input)
    {
        _movementInput = new Vector3(input.x, 0f, input.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetVelocity = _movementInput * moveSpeed;
        Vector3 velocity = new Vector3(targetVelocity.x, _rb.linearVelocity.y, targetVelocity.z);
        
        _rb.linearVelocity = velocity;
    }
}
