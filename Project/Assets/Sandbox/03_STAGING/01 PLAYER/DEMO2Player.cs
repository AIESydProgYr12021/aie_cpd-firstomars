using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.Player
{
    public class DEMO2Player : MonoBehaviour
    {
        
        [SerializeField] float speed = 2.0f;
        [SerializeField] float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;


        [SerializeField] Transform cam;
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
            
            //THIRD PERSON CAMERA - NOT LOCKED

            /*
            Vector3 direction = new Vector3(joystickController.Direction.x, 0, joystickController.Direction.y);

            //move / rotate player only when there is player input
            if (direction.x != 0 || direction.z != 0)
            {
                //find angle of turn
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                
                //smooth turn
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                
                //rotate player
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //move player
                rb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
                //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                //rb.AddForce(direction);
            }
            */

            //THIRD PERSON CAMERA - LOCKED
            Vector3 direction = new Vector3(joystickController.Direction.x, 0, joystickController.Direction.y);

            //move / rotate player only when there is player input
            if (direction.x != 0 || direction.z != 0)
            {
                //find angle of turn
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                //smooth turn
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

                //rotate player
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //move player
                rb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
                //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                //rb.AddForce(direction);
            }

        }
    }
}

