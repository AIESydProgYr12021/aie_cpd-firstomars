using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.Ammo
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject ammoPrefab;
        

        private void Start()
        {
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                //Instantiate(ammoPrefab, transform);
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }
}
