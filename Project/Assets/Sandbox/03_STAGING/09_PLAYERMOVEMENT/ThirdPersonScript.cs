using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.PlayerMovement
{
    public class ThirdPersonScript : MonoBehaviour
    {
        [SerializeField] CharacterController controller;
        [SerializeField] float speed = 6f;
        [SerializeField] float rotateSpeed = 0.5f;
        [SerializeField] float rotationSpeedMax = 0.3f;

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(0f, 0f, vertical).normalized;
            Vector3 rotation = new Vector3(0f, horizontal, 0f).normalized;

            if (direction.magnitude >= 0.1f || rotation.magnitude >= 0.1f)
            {
                if (rotation.y > rotationSpeedMax) rotation.y = rotationSpeedMax;
                if (rotation.y < -rotationSpeedMax) rotation.y = -rotationSpeedMax;

                transform.Rotate(rotation);

                Vector3 movement = transform.rotation * direction;
                controller.Move(movement * speed * Time.deltaTime);
            }
        }
    }
}
