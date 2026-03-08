using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    private Dictionary<GameObject, ObjectPool<EnemyHealth>> _pools = 
        new Dictionary<GameObject, ObjectPool<EnemyHealth>>();
    
    public EnemyHealth SpawnEnemy(GameObject enemyPrefab)
    {
        if (!_pools.ContainsKey(enemyPrefab))
        {
            EnemyHealth enemyComponent = enemyPrefab.GetComponent<EnemyHealth>();

            _pools[enemyPrefab] = new ObjectPool<EnemyHealth>(enemyComponent, 10, transform);
        }

        ObjectPool<EnemyHealth> pool = _pools[enemyPrefab];
        
        EnemyHealth enemy = pool.Get();

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawn = spawnPoints[index];
        
        enemy.GetComponent<Enemy>().ResetEnemy();
        
        enemy.transform.position = spawn.position;

        return enemy;
    }
}