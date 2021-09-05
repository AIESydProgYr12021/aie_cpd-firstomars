using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.PlayerEnemy
{
    public class SpawnProjectiles_V2 : MonoBehaviour
    {
        public GameObject firePoint;
        public List<GameObject> vfx = new List<GameObject>();

        private GameObject effectToSpawn;
        private Vector3 playerMoveDir;
        private Quaternion testplayerMoveDir;
        private float timeToFire = 0;

        // Start is called before the first frame update
        void Start()
        {
            effectToSpawn = vfx[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0) && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove_V2>().fireRate;
                SpawnVFX();
            }
        }

        void SpawnVFX()
        {
            GameObject vfx; //what is this exactly?

            if (firePoint != null)
            {
                vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);

                vfx.transform.localRotation = GetPlayerRotation();
                //vfx.transform.localRotation = testplayerMoveDir;
            }
            else
            {
                Debug.Log("No fire point");
            }
        }

        private Quaternion GetPlayerRotation()
        {
            playerMoveDir = GetComponentInParent<PlayerController_V2>().PlayerMoveDirection;

            return testplayerMoveDir = GetComponentInParent<Transform>().rotation;

            //return testplayerMoveDir = Quaternion.LookRotation(playerMoveDir);
        }
    }
}

