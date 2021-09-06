using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.PlayerEnemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        public GameObject player;
        private PlayerController_V2 playerController;
        private int attackDamage;

        // Start is called before the first frame update
        private void Start()
        {
            playerController = player.GetComponent<PlayerController_V2>();
            attackDamage = GetComponentInParent<EnemyController>().attackDamage;
        }

        public void DamagePlayer()
        {
            playerController.AttackedByEnemy(attackDamage);
            Debug.Log("player attacked - damage delivered: " + attackDamage);
        }
    }
}
