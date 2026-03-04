using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image heathFill;

    private void Start()
    {
        playerHealth.OnHealthChange += UpdateHealthBar;
    }

    private void UpdateHealthBar(int current, int max)
    {
        float value = (float)current / max;
        heathFill.fillAmount = value;
    }
}
