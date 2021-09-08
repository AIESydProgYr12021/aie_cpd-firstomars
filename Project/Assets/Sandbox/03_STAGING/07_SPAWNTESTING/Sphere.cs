using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Staging.SpawnTesting
{
    public class Sphere : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(gameObject.name + " destroyed");
            Destroy(gameObject);
        }
    }
}

