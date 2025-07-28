using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int minEnemiesPerBurst = 3;
    public int maxEnemiesPerBurst = 7;
    public float minTimeBetweenBursts = 1f;
    public float maxTimeBetweenBursts = 3f;
    public float spawnRadius = 5f;
    public int maxEnemiesInRadius = 20;
    public float timeBetweenWaves = 5f; // Time between waves
    public int maxWaves = 3;  // Number of waves

    private int currentWave = 0;
    private int enemiesInRadius = 0;
    private float nextWaveTime;

    void Start()
    {
        nextWaveTime = Time.time + timeBetweenWaves;
    }

    void Update()
    {
        if (Time.time >= nextWaveTime && currentWave < maxWaves)
        {
            StartCoroutine(SpawnWave());
            currentWave++;
            nextWaveTime = Time.time + timeBetweenWaves;
        }
    }

    IEnumerator SpawnWave()
    {
        enemiesInRadius = 0;
        int enemiesToSpawn = Random.Range(minEnemiesPerBurst, maxEnemiesPerBurst + 1);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (enemiesInRadius < maxEnemiesInRadius)
            {
                SpawnEnemy();
                enemiesInRadius++;
            }
            else
            {
                break; // Stop spawning if max is reached
            }
            yield return new WaitForSeconds(Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts));
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
    public void EnemyDefeated()
    {
        enemiesInRadius--;
    }

}