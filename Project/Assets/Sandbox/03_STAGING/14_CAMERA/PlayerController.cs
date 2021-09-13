using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Staging.Camera
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController cc;

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(0f, 0f, vertical);
            Vector3 rotation = new Vector3(0f, horizontal, 0f);
            transform.Rotate(rotation);

            Vector3 movement = transform.rotation * direction;
            cc.Move(movement * 10f * Time.deltaTime);
            

        }
    }
}