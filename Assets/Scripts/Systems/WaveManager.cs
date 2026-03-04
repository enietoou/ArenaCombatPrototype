using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int _aliveEnemies;
    
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float spawnRadius = 10f;

    private void Start()
    {
        StartNextWave();
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
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector2 circle = Random.insideUnitCircle * spawnRadius;

            Vector3 spawnPos = new Vector3(circle.x, 1f, circle.y);

            spawner.SpawnEnemy(spawnPos);
        }

        enemiesPerWave += 2;
    }
}