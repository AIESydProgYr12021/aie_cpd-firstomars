using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthPackPrefab;
    [SerializeField] Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        //int numSpawnPos = spawnLocations.Length;
    }

    public void SpawnHealth()
    {
        Instantiate(healthPackPrefab, ReturnRandomSpawnPoint().position, Quaternion.identity);
        //Instantiate(healthPackPrefab, spawnLocations[0].position, Quaternion.identity);
        Debug.Log("Health pack spawned at ");
    }

    private Transform ReturnRandomSpawnPoint()
    {
        return spawnLocations[Random.Range(0, spawnLocations.Length)];
    }

}
