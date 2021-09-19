using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healAmount;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timer;

    private ThirdPersonScript playerController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = PlayerManager.instance.player;
        playerController = player.GetComponent<ThirdPersonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f);

        timer -= Time.deltaTime;
        if (timer <= 0) HealthDisappears();
    }

    private void HealthDisappears()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player gets health pack");
            playerController.HealthPackReceived(healAmount);
            HealthDisappears();
        }
    }
}
