using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private MissionCompleteUI missionCompleteUI;

    private bool _isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);
        
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void Update()
    {
        if (gameOverUI != null && gameOverUI.IsGameOver()) return;
        
        if (missionCompleteUI != null && missionCompleteUI.IsMissionComplete()) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        
        pausePanel.SetActive(_isPaused);
        
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    public void Resume()
    {
        _isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
