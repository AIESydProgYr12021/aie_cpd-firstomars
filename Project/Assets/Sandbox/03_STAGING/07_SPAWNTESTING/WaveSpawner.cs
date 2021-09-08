using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.SpawnTesting
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] enum SpawnState
        {
            SPAWNING,
            WAITING,
            COUNTING
        };

        [System.Serializable] //this allows the wave variables to be edited in inspector
        public class Wave
        {
            public string name;
            public Transform enemy;
            public int count;
            public float rate;
        }

        [SerializeField] Transform[] waveSpawners; // REPLACE
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
                if (!isEnemiesAlive())  WaveCompleted();
                else                    return; // skips all logic below if there are still enemies
            }
            
            if (waveCountdown <= 0f)
            {
                //if not already spawning
                if (state != SpawnState.SPAWNING)  StartCoroutine(SpawnWave(waves[nextWave])); //spawn wave
            }
            else waveCountdown -= Time.deltaTime;
        }

        private void WaveCompleted()
        {
            Debug.Log("Wave completed");
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;
            if(nextWave + 1 > waves.Length - 1)
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
            Debug.Log("Spawning Wave: " + _wave.name);
            state = SpawnState.SPAWNING;

            //spawn enemies
            for (int i = 0; i < _wave.count; i++)
            {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds(1f / _wave.rate);
            }

            state = SpawnState.WAITING;

            yield break; //always end with yield break
        }

        void SpawnEnemy(Transform _enemy)
        {
            //spawn enemy
            Instantiate(_enemy, waveSpawners[0].position, transform.rotation);
            Debug.Log("Spawn enemy: " + _enemy.name);
        }
    }
}