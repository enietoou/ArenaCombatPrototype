using System;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesText;

    private void OnEnable()
    {
        waveManager.OnWaveStarted += UpdateWave;
        waveManager.OnEnemyCountChanged += UpdateEnemyCount;
    }

    private void OnDisable()
    {
        waveManager.OnWaveStarted -= UpdateWave;
        waveManager.OnEnemyCountChanged -= UpdateEnemyCount;
    }

    private void UpdateWave(int wave)
    {
        waveText.text = $"Wave: {wave}";
    }

    private void UpdateEnemyCount(int count)
    {
        enemiesText.text = $"Enemies: {count}";
    }
}
