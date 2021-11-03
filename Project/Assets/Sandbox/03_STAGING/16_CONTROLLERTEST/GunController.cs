using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.Controller
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootGun();
            }
        }

        private void ShootGun()
        {
            GameObject bullet;
            
            bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.transform.localRotation = GetPlayerRotation();
        }

        private Quaternion GetPlayerRotation()
        {
            //playerMoveDir = GetComponentInParent<ThirdPersonScript>().PlayerMoveDirection;

            Quaternion playerMoveDir = GetComponentInParent<Transform>().rotation;

            return playerMoveDir;

            //return { 0f, playerMoveDir.y, 0f};
        }

    }

}
