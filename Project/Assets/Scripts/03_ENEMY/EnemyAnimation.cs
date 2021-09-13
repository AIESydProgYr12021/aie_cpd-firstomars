using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private ThirdPersonScript playerController;
    private int attackDamage;

    private void Start()
    {
        GameObject player = PlayerManager.instance.player;
        playerController = player.GetComponent<ThirdPersonScript>();
        attackDamage = GetComponentInParent<EnemyController>().attackDamage;
    }

    public void DamagePlayer()
    {
        playerController.AttackedByEnemy(attackDamage);
        Debug.Log("player attacked - damage delivered: " + attackDamage);
    }
}