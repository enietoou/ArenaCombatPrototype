using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupFloatAnimation : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.25f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float rotationSpeed = 90f;
    
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        Float();
        Rotate();
    }

    private void Float()
    {
        float newY = _startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        
        transform.position = new Vector3(_startPosition.x, newY, transform.position.z);
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
