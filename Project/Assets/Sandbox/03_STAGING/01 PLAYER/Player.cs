//using SandBox.Staging.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 2.0f;
        [SerializeField] VirtualJoystick joystick;
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void JoystickMove(Vector2 dir)
        {
            Vector3 direction = new Vector3(dir.x, 0, dir.y);
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            //rb.AddForce(direction * speed);
        }
    }

}
