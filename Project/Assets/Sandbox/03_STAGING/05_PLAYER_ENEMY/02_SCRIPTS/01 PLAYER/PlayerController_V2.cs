using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SandBox.Staging.PlayerEnemy
{
    public class PlayerController_V2 : MonoBehaviour
    {
        //health
        [SerializeField] int playerHealth;
        
        //movement
        [SerializeField] float speed = 2.0f;
        [SerializeField] float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        [SerializeField] Transform cam;
        [SerializeField] GameObject joystick;
        private VirtualJoystickController_V2 joystickController;
        private CharacterController cc;

        public Vector3 PlayerMoveDirection { get; private set; }

        //animation
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            cc = GetComponent<CharacterController>();
            joystickController = joystick.GetComponent<VirtualJoystickController_V2>();
            anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //THIRD PERSON CAMERA - LOCKED
            Vector3 direction = new Vector3(joystickController.Direction.x, 0, joystickController.Direction.y);

            //move / rotate player only when there is player input
            if (direction.x != 0 || direction.z != 0)
            {
                anim.SetBool("isWalking", true);
                
                //find angle of turn
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                //smooth turn
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

                //rotate player
                //transform.rotation = Quaternion.Euler(0f, angle, 0f);

                PlayerMoveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                cc.Move(PlayerMoveDirection.normalized * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            if (playerHealth <= 0) Destroy(gameObject);
        }

        public void AttackedByEnemy(int attackDamage)
        {
            playerHealth -= attackDamage;
        }
    }
}