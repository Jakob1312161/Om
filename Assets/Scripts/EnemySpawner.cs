using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Spawn Time")]
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(0f, 1000f);
        float z = Random.Range(0f, 1000f);

        Vector3 spawnPosition = new Vector3(x, 1.96f, z);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}