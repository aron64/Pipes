using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rb;
    float positionWasX = 0, positionWasY = 0, speed = 5f, jump = 4f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-1,0,0) * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(1,0,0) * speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0,0,1) * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0,0,-1) * speed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(0,1,0) * jump;
        }
    }
}
