using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject gameOverPanel;
    
    private bool _isGameOver;
    
    private void Start()
    {
        gameOverPanel.SetActive(false);
        playerHealth.OnDeath += ShowGameOver;
    }

    private void ShowGameOver()
    {
        _isGameOver = true;
        
        gameOverPanel.SetActive(true);
        
        Time.timeScale = 0;
    }

    public void Respawn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    private void OnDestroy()
    {
        playerHealth.OnDeath -= ShowGameOver;
    }
}
