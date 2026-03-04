using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Transform[] spawnPoints;
    
    public EnemyHealth SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        
        Transform spawn = spawnPoints[index];
        
        GameObject enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

        return enemy.GetComponent<EnemyHealth>();
    }
}