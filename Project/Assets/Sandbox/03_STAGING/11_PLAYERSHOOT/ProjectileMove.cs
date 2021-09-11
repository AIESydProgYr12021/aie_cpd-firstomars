using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.PlayerShoot
{
    public class ProjectileMove : MonoBehaviour
    {
        public float speed;
        public float fireRate;
        public GameObject muzzlePrefab;
        public GameObject hitPrefab;

        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            if (muzzlePrefab != null)
            {
                GameObject muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.forward = gameObject.transform.forward; //how to set to forward in direction of gun?

                var particleSysMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
                if (particleSysMuzzle != null)
                {
                    Destroy(muzzleVFX, particleSysMuzzle.main.duration);
                }
                else
                {
                    var particleSysChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(muzzleVFX, particleSysChild.main.duration);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (speed != 0)
            {
                // transform.position += transform.forward * (speed + Time.deltaTime);
                // rb.MovePosition(transform.position + transform.forward * (speed + Time.deltaTime)); -- TEST
                rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Projectile speed is 0");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Weapon")
            {
                speed = 0;

                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                if (hitPrefab != null)
                {
                    GameObject hitVFX = Instantiate(hitPrefab, pos, rot);

                    var particleSysHit = hitVFX.GetComponent<ParticleSystem>();
                    if (particleSysHit != null)
                    {
                        Destroy(hitVFX, particleSysHit.main.duration);
                    }
                    else
                    {
                        var particleSysChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitVFX, particleSysChild.main.duration);
                    }
                }

                Destroy(gameObject);
            }
        }
    }
}

