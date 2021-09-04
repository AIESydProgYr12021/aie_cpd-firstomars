using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillMovementController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //only move player on x and z axis, not y
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
