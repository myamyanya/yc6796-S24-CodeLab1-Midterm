using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float forceAmt = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //rb.MovePosition(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z));
            rb.AddForce(Vector3.left * forceAmt);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * forceAmt);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * forceAmt);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(Vector3.back * forceAmt);
        }
        
    }
}
