using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.Ammo
{
    public class TESTPlayerManager : MonoBehaviour
    {

        #region Singleton

        public static TESTPlayerManager instance;

        private void Awake()
        {
            instance = this;
        }

        #endregion

        public GameObject player;

    }
}

