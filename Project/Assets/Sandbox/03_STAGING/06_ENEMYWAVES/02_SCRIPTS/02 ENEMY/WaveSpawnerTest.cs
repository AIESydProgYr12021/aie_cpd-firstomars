using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerTest : MonoBehaviour
{
    [SerializeField] enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Enemy[] enemies;

        //public Tuple<Transform, int>[] enemyPrefabCount;???

        [System.Serializable]
        public class Enemy
        {
            public Transform enemy;
            //public Transform spawnLocation; //make an array - choose random
            public Transform[] spawnLocations;
            public int count;
            public float rate;
        }
    }

    [SerializeField] Wave[] waves;
    private int nextWave = 0;

    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float waveCountdown;

    private float searchCountdown = 1f;

    [SerializeField] SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!isEnemiesAlive()) WaveCompleted();
            else return; // skips all logic below if there are still enemies
        }

        if (waveCountdown <= 0f)
        {
            //if not already spawning
            if (state != SpawnState.SPAWNING) StartCoroutine(SpawnWave(waves[nextWave])); //spawn wave
        }
        else waveCountdown -= Time.deltaTime;
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete - looping");
        }
        else nextWave++;
    }

    bool isEnemiesAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f; //reset searchcountdown
            if (GameObject.FindGameObjectWithTag("Enemy") == null) return false;
        }
        return true;
    }

    //create methods after waiting a number of secs (different from invoke?)
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        //enemyTypeNum = the tpye of enemy number
        //en - the individual enemy number

        for (int enemyTypeNum = 0; enemyTypeNum < _wave.enemies.Length; enemyTypeNum++)
        {
            for (int en = 0; en < _wave.enemies[enemyTypeNum].count; en++)
            {
                //          enemy prefab                        random enemy spawn position
                SpawnEnemy(_wave.enemies[enemyTypeNum].enemy, ReturnRandomSpawnPoint(_wave.enemies[enemyTypeNum].spawnLocations));
                yield return new WaitForSeconds(1f / _wave.enemies[enemyTypeNum].rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    private Transform ReturnRandomSpawnPoint(Transform[] _spawnPositions)
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Length)];
    }

    void SpawnEnemy(Transform _enemy, Transform _spawnPos)
    {
        //spawn enemy
        Instantiate(_enemy, _spawnPos.position, transform.rotation);
        Debug.Log("Spawn enemy: " + _enemy.name + " at " + _spawnPos.name);
    }
}