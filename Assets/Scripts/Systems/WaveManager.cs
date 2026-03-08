using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private PlayerHealth playerHealth;
    
    public event Action<int> OnWaveStarted;
    public event Action<int> OnEnemyCountChanged;
    public event Action OnAllWavesCompleted;
    
    private int _aliveEnemies;
    private int _currentWaveIndex;
    
    private bool _gameOver;

    private void Start()
    {
        playerHealth.OnDeath += HandlePlayerDeath;
        
        StartNextWave();
    }

    private void HandlePlayerDeath()
    {
        _gameOver = true;
        
        Time.timeScale = 0f;
    }
    
    private void StartNextWave()
    {
        if (_gameOver) return;
        
        if (_currentWaveIndex >= waveConfig.waves.Length)
        {
            OnAllWavesCompleted?.Invoke();
            return;
        }
        
        WaveData wave = waveConfig.waves[_currentWaveIndex];
        
        OnWaveStarted?.Invoke(_currentWaveIndex + 1);

        foreach (var enemyGroup in wave.enemies)
        {
            for (int i = 0; i < enemyGroup.count; i++)
            {
                EnemyHealth enemy = spawner.SpawnEnemy(enemyGroup.enemyPrefab);
                
                RegisterEnemy(enemy);
            }
        }
        
        _currentWaveIndex++;
    }

    public void RegisterEnemy(EnemyHealth enemy)
    {
        _aliveEnemies++;
        
        OnEnemyCountChanged?.Invoke(_aliveEnemies);

        enemy.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyHealth enemy)
    {
        _aliveEnemies--;
        
        OnEnemyCountChanged?.Invoke(_aliveEnemies);

        enemy.OnDeath -= HandleEnemyDeath;

        if (_aliveEnemies <= 0)
        {
            StartNextWave();
        }
    }

    private void OnDestroy()
    {
        playerHealth.OnDeath -= HandlePlayerDeath;
    }
}