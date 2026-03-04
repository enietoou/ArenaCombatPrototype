using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TMP_Text gameOverText;

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        playerHealth.OnDeath += ShowGameOver;
    }

    private void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
