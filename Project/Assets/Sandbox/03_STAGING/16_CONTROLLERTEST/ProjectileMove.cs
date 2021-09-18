using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.Controller
{ 
    public class ProjectileMove : MonoBehaviour
    {
        [SerializeField] private float liveTime = 3.0f;
        [SerializeField] private float speed = 5.0f;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
    }
}
