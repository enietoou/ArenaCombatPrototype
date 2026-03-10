using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        AudioListener.pause = true;
    }

    public void Respawn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioListener.pause = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
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
