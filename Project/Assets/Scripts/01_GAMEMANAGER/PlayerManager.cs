using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace SandBox.Staging.EnemyWaves
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public bool isGamePaused = false;

    private void Update()
    {
        if (isGamePaused) Pause();
        else Resume();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
    }

    

    //public void OnApplicationPause(bool pause)
    //{
    //    if (pause) Time.timeScale = 0f;
    //    else Time.timeScale = 1f;
    //}

}

