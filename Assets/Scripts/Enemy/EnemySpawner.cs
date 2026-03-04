using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private WaveManager waveManager;

    public void SpawnEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);

        EnemyHealth enemy = enemyObj.GetComponent<EnemyHealth>();

        waveManager.RegisterEnemy(enemy);
    }
}