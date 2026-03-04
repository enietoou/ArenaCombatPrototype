using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int _aliveEnemies;
    
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private PlayerHealth playerHealth;
    
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
        
        Debug.Log("Game Over");
    }

    public void RegisterEnemy(EnemyHealth enemy)
    {
        _aliveEnemies++;

        enemy.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyHealth enemy)
    {
        _aliveEnemies--;

        enemy.OnDeath -= HandleEnemyDeath;

        if (_aliveEnemies <= 0)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        if (_gameOver) return;
        
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector2 circle = Random.insideUnitCircle * spawnRadius;

            Vector3 spawnPos = new Vector3(circle.x, 1f, circle.y);

            spawner.SpawnEnemy(spawnPos);
        }

        enemiesPerWave += 2;
    }
}