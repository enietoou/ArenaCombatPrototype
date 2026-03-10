using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCompleteUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject missionCompletePanel;
    
    private bool _isMissionComplete;

    private void Start()
    {
        missionCompletePanel.SetActive(false);
        waveManager.OnAllWavesCompleted += ShowPanel;
    }

    private void ShowPanel()
    {
        _isMissionComplete = true;
        
        missionCompletePanel.SetActive(true);
        
        Time.timeScale = 0;
        
        AudioListener.pause = true;
    }

    public void PlayAgain()
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

    public bool IsMissionComplete()
    {
        return _isMissionComplete;
    }
    
    private void OnDestroy()
    {
        waveManager.OnAllWavesCompleted -= ShowPanel;
    }
}
