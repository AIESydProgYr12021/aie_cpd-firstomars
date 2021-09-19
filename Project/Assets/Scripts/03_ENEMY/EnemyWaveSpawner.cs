using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject countDown;
    private CoundownController countDownController;
    
    [SerializeField]
    enum SpawnState
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
            [SerializeField, ContainsAtLeast(3)] public string enemyName;
            public Transform enemy;
            [SerializeField, NotNull] public Transform[] spawnLocations;
            public int count;
            public float rate;
        }
    }

    [SerializeField] Wave[] waves;
    private int nextWave = 0;
    private float searchCountdown = 1f; //how often should search for enemies?

    [SerializeField] SpawnState state = SpawnState.COUNTING;

    private GameManager gameManager;
    private HealthPackSpawner healthSpawner;
    private bool wavesCompleted = false;


    private void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
        healthSpawner = gameManager.GetComponentInChildren<HealthPackSpawner>();
        countDownController = countDown.GetComponent<CoundownController>();
        StartCoroutine(StartNextCountdown());
    }

    private void Update()
    {
        if (state == SpawnState.WAITING && !wavesCompleted)
        {
            if (!isEnemiesAlive()) WaveCompleted();
            else return; // skips all logic below if there are still enemies
        }
    }

    public void StartWave()
    {
        //triggered by countdown object - see inspector
        StartCoroutine(SpawnWave(waves[nextWave]));
    }

    private void WaveCompleted()
    {
        //Debug.Log("Wave completed");
        state = SpawnState.COUNTING;

        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            //Debug.Log("All waves complete");
            wavesCompleted = true;
            gameManager.WinPrompt();

        }
        else
        {
            nextWave++;
            StartCoroutine(StartNextCountdown());
        }
    }

    IEnumerator StartNextCountdown()
    {
        healthSpawner.SpawnHealth();
        countDownController.SetWaveIndex(nextWave);

        yield return new WaitForSeconds(3f);
        countDown.SetActive(true);
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        //Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        //enemyTypeNum = the tpye of enemy number & en - the individual enemy number
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

    private void SpawnEnemy(Transform _enemy, Transform _spawnPos)
    {
        //spawn enemy
        Instantiate(_enemy, _spawnPos.position, transform.rotation);
        //Debug.Log("Spawn enemy: " + _enemy.name + " at " + _spawnPos.name);
    }

    private bool isEnemiesAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f; //reset searchcountdown
            if (GameObject.FindGameObjectWithTag("Enemy") == null) return false;
        }
        return true;
    }
}
