using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Staging.Joystick
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private VirtualJoystick inputSource;
        private Rigidbody rb;
    
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.velocity = new Vector3(inputSource.Direction.x, 0, inputSource.Direction.y);
        }
    }
}
