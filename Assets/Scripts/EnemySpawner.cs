using UnityEngine;
using System.Collections;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text waveText;
    public TMP_Text enemiesText;

    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Spawn Area")]
    public float minX = -100f;
    public float maxX = 100f;
    public float minZ = -100f;
    public float maxZ = 100f;

    [Header("Spawn Time")]
    public float spawnDelay = 1f;

    [Header("Wave Settings")]
    public int currentWave = 1;
    public int enemiesPerWave = 5;
    public float enemyIncreasePerWave = 3;

    private int enemiesAlive = 0;

    void Start()
    {
        UpdateUI();
        StartCoroutine(StartWave());
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        Debug.Log("Wave " + currentWave + " startet!");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(x, 1.96f, z);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemiesAlive++;
        UpdateUI();

        Target target = enemy.GetComponent<Target>();

        if (target != null)
        {
            target.spawner = this;
        }
    }

    public void EnemyKilled()
    {
        enemiesAlive--;
        UpdateUI();

        if (enemiesAlive <= 0)
        {
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        currentWave++;
        UpdateUI();

        enemiesPerWave += (int)enemyIncreasePerWave;

        StartCoroutine(NextWaveDelay());
    }

    IEnumerator NextWaveDelay()
    {
        Debug.Log("Nächste Wave in 5 Sekunden...");

        yield return new WaitForSeconds(5f);

        StartCoroutine(StartWave());
    }

    void UpdateUI()
    {
        if (waveText != null)
            waveText.text = "Wave: " + currentWave;

        if (enemiesText != null)
            enemiesText.text = "Gegner: " + enemiesAlive;
    }
}