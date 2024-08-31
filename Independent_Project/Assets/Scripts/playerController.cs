using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float jumpHeight = 1f;
    public float speed = 1f;
    private float lrInput;
    private float udInput;
    private float jumpInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lrInput = Input.GetAxis("Horizontal");
        //changed Input Manager and switched Negative and Positive Buttons 
        udInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
        transform.Translate(Vector3.forward* Time.fixedDeltaTime * speed * lrInput);
        transform.Translate(Vector3.right * Time.fixedDeltaTime * speed * udInput);
        transform.Translate(Vector3.up * Time.deltaTime * jumpHeight * jumpInput);
    }
}
