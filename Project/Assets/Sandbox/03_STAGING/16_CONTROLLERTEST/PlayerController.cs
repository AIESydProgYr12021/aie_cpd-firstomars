using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController cc;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootGun();
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(0f, 0f, vertical);
            Vector3 rotation = new Vector3(0f, horizontal, 0f);

            transform.Rotate(rotation);

            Vector3 movement = transform.rotation * direction;

            cc.Move(movement * 10f * Time.deltaTime);
        }

        public void ShootGun()
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }

    }
}
