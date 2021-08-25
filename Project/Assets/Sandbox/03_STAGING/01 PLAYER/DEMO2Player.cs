using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.Player
{
    public class DEMO2Player : MonoBehaviour
    {
        public float speed = 2.0f;
        [SerializeField] GameObject joystick;
        private DEMO2VirtualJoystick joystickController;
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            joystickController = joystick.GetComponent<DEMO2VirtualJoystick>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 direction = new Vector3(joystickController.Direction.x, 0, joystickController.Direction.y);
            //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            rb.AddForce(direction);
        }
    }
}

