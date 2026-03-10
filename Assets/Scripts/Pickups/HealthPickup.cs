using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 30;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        
        if (playerHealth == null) return;
        
        playerHealth.Heal(healAmount);
        
        Destroy(gameObject);
        
        if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position);
    }
}
