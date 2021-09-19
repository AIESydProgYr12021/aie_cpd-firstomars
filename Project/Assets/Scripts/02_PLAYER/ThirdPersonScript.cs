using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonScript : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject gameManager;
    private GameManager gameManagerController;
    [SerializeField] private GameObject joystick;
    private VirtualJoystick joystickController;
    private SpawnProjectiles spawnProjs;

    [Header("Player Health")]
    [SerializeField] float maxPlayerHealth;
    [SerializeField] float playerHealth;
    [SerializeField] Slider healthSlider;

    [Header("Player Movement")]
    [SerializeField] float speed = 6f;
    [SerializeField] private float rotationSpeedMax = 0.7f;
    [SerializeField] private float movementSensivity = 0.05f;
    [SerializeField] private float rotationSensitivity = 0.3f;

    private float gravity = 9.8f;
    private float vertSpeed = 0;

    public Vector3 PlayerMoveDirection { get; private set; }

    private void Start()
    {
        gameManagerController = gameManager.GetComponent<GameManager>();
        joystickController = joystick.GetComponent<VirtualJoystick>();
        spawnProjs = GetComponentInChildren<SpawnProjectiles>();
        playerHealth = maxPlayerHealth;
        healthSlider.value = CalculateHealth();
    }

    void Update()
    {
        float horizontal = joystickController.Direction.x;
        float vertical = joystickController.Direction.y;

        Vector3 direction = new Vector3(0f, 0f, vertical);
        Vector3 rotation = new Vector3(0f, horizontal, 0f);

        if (direction.magnitude >= movementSensivity)
        {
            Vector3 movement = transform.rotation * direction;

            //apply gravity
            movement.y = vertSpeed;

            controller.Move(movement * speed * Time.deltaTime);
        }

        if (rotation.magnitude >= rotationSensitivity)
        {
            if (rotation.y > rotationSpeedMax) rotation.y = rotationSpeedMax;
            if (rotation.y < -rotationSpeedMax) rotation.y = -rotationSpeedMax;

            transform.Rotate(rotation);
        }

        //calculate gravity
        if (controller.isGrounded) vertSpeed = 0;
        vertSpeed -= gravity * Time.deltaTime;

        if (playerHealth <= 0)
        {
            gameManagerController.LosePrompt();
            Destroy(gameObject);
        }
    }

    private float CalculateHealth()
    {
        return playerHealth / maxPlayerHealth;
    }

    public void HealthPackReceived(float healAmount)
    {
        Debug.Log("Health pack received " + healAmount);
        if (playerHealth + healAmount > maxPlayerHealth) playerHealth = maxPlayerHealth;
        else playerHealth += healAmount;
        healthSlider.value = CalculateHealth();
    }

    public void AttackedByEnemy(int attackDamage)
    {
        playerHealth -= attackDamage;
        healthSlider.value = CalculateHealth();
    }

    public void SpawnAmmo(Vector3 spawnPos)
    {
        spawnProjs.SpawnAmmo(spawnPos);
    }

    public void AmmoPickUp(int ammoAmt)
    {
        spawnProjs.IncreaseAmmo(ammoAmt);
    }

    public int GetAmmoAmount()
    {
        return spawnProjs.CurrentAmmo;
    }

    public void DecAmmoSpwnCntr()
    {
        spawnProjs.DecreaseAmmoSpawnCounter();
    }


}