using System;
using TMPro;
using UnityEngine;

public class MissionCompleteUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TMP_Text missionCompleteText;

    private void OnEnable()
    {
        waveManager.OnAllWavesCompleted += ShowPanel;
    }

    private void OnDisable()
    {
        waveManager.OnAllWavesCompleted -= ShowPanel;
    }

    private void ShowPanel()
    {
        missionCompleteText.gameObject.SetActive(true);
        
        Time.timeScale = 0;
    }
}
