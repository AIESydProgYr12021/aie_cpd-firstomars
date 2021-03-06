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
        //private Rigidbody rb;

        private CharacterController cc;

        Animator anim;

        //private Vector3 moveDir; -- TO DELETE replaced by PlayerMoveDirection
        public Vector3 PlayerMoveDirection { get; private set; }


        // Start is called before the first frame update
        void Start()
        {
            //rb = GetComponent<Rigidbody>();
            cc = GetComponent<CharacterController>();
            joystickController = joystick.GetComponent<DEMO2VirtualJoystick>();

            //anim = GetComponentInChildren<Animator>();

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
            //THIRD PERSON CAMERA - LOCKED
            Vector3 direction = new Vector3(joystickController.Direction.x, 0, joystickController.Direction.y);

            //move / rotate player only when there is player input
            if (direction.x != 0 || direction.z != 0)
            {
                //anim.SetBool("isWalking", true);
                
                //find angle of turn
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                //smooth turn
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

                //rotate player
                //transform.rotation = Quaternion.Euler(0f, angle, 0f);

                PlayerMoveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //move player
                //rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
                //rb.MovePosition(transform.position + PlayerMoveDirection.normalized * speed * Time.deltaTime);

                cc.Move(PlayerMoveDirection.normalized * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //Debug.Log(PlayerMoveDirection.normalized);


                //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                //rb.AddForce(direction);
            }
            else
            {
                //anim.SetBool("isWalking", false);
            }

        }
    }
}

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
