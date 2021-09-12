using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.Ammo
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] private int ammoAmount;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float timer;

        private GameObject player;
        private ThirdPersonScript playerController;

        private void Start()
        {
            player = TESTPlayerManager.instance.player;
            playerController = player.GetComponent<ThirdPersonScript>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            timer -= Time.deltaTime;

            if (timer <= 0) Destroy(gameObject);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerController.AmmoPickUp(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
