using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace SandBox.Staging.EnemyWaves
public class EnemyAnimation : MonoBehaviour
{
    //[SerializeField] GameObject player;
    private ThirdPersonScript playerController;
    private int attackDamage;

    // Start is called before the first frame update
    private void Start()
    {
        //playerController = player.GetComponent<PlayerController_V2>();

        GameObject player = PlayerManager.instance.player;
        playerController = player.GetComponent<ThirdPersonScript>();


        //playerController = GetComponentInParent<EnemyController>().GetComponent<PlayerController_V2>();

        attackDamage = GetComponentInParent<EnemyController>().attackDamage;

        //DELETE - DIDN'T WORK
        //playerController = GetComponentInParent<EnemyController>().player.gameObject.GetComponent<PlayerController_V2>();
    }

    public void DamagePlayer()
    {
        playerController.AttackedByEnemy(attackDamage);
        Debug.Log("player attacked - damage delivered: " + attackDamage);
    }
}