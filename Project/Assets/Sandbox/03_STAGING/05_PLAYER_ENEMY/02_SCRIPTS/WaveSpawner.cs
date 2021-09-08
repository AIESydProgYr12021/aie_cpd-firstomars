using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Transform enemyPrefab;

    [SerializeField] float timeBetweenEnemyWaves = 5f;
    private float countdown = 2f;

    [SerializeField] int numberOfWaves;
    [SerializeField] List<Vector3> spawnPoints;

    private void Update()
    {
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenEnemyWaves;
        }

        // time that has passed since last time we passed a frame
        countdown -= Time.deltaTime; 
    }

    private void SpawnWave()
    {
        Debug.Log("Wave incoming");
    }

}
