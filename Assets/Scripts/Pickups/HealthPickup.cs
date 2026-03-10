using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        
        if (playerHealth == null) return;
        
        playerHealth.Heal(healAmount);
        
        Destroy(gameObject);
    }
}
