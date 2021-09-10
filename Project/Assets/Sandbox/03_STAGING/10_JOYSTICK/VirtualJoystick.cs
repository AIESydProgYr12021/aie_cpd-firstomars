using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sandbox.Staging.PlayerMovement
{
    public class VirtualJoystick : MonoBehaviour//, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private float joystickVisualDistance = 50;
        private Image container;
        private Image joystick;

        private Vector3 direction;
        public Vector3 Direction { get { return direction; } }



        [SerializeField] private VirtualJoystick inputSource;
        private Rigidbody rigid;
        
        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rigid.velocity = inputSource.Direction;
        }
    }
}