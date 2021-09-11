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
}

