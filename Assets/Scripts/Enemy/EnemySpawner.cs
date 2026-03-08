using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    
    public EnemyHealth SpawnEnemy(GameObject enemyPrefab)
    {
        int index = Random.Range(0, spawnPoints.Length);
        
        Transform spawn = spawnPoints[index];
        
        GameObject enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

        return enemy.GetComponent<EnemyHealth>();
    }
}