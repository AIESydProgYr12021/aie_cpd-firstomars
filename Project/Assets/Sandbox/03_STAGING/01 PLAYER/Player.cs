using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;
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
        rb.AddForce(direction * speed);
    }
}
