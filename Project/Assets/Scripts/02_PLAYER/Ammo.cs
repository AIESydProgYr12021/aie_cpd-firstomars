using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timer;

    private GameObject player;
    private ThirdPersonScript playerController;

    private void Start()
    {
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 25f;
        rotationVector.z = 25f;
        transform.rotation = Quaternion.Euler(rotationVector);

        player = PlayerManager.instance.player;
        playerController = player.GetComponent<ThirdPersonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
        timer -= Time.deltaTime;

        if (timer <= 0) AmmoDisappears();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.AmmoPickUp(ammoAmount);
            AmmoDisappears();
        }
    }

    private void AmmoDisappears()
    {
        playerController.DecAmmoSpwnCntr();
        Destroy(gameObject);
    }

}
