using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float jumpHeight = 2.5f;
    public float speed = 2.0f;
    private float lrInput;
    private float udInput;
    private float jumpInput;
    public GameObject projectilePrefab;
    private bool isGrounded;
    private bool isBlocking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!isBlocking)
        { 
        lrInput = Input.GetAxis("Horizontal");
        udInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");

        // Calculate movement direction
        //.normalized is used to 
        Vector3 movementDirection = new Vector3(-udInput, 0, lrInput).normalized;

        // Rotate the character based on movement direction
        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }

        // Translate the character in the direction it is facing
        // Player seems to slide upon exiting key press, look for 
        // ways to make movement more precise
        Vector3 movement = movementDirection * Time.fixedDeltaTime * speed;
        transform.Translate(movement, Space.World);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Handle jumping
        if (isGrounded && jumpInput > 0)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

            // Instantiate projectile
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Projectile fired!");
                if (movementDirection != Vector3.zero)
                {
                    Quaternion projectileRotation = Quaternion.LookRotation(movementDirection);
                    Vector3 spawnPosition = transform.position + movementDirection * 1.5f;
                    Instantiate(projectilePrefab, spawnPosition, projectileRotation);
                }
                else
                {
                    Vector3 spawnPosition = transform.position + transform.forward * 1.5f;
                    // If no movement direction, instantiate with player's current rotation
                    Instantiate(projectilePrefab, spawnPosition, transform.rotation);
                }
            }
        }


    }

    public void SetBlockingState(bool blocking)
    {
        isBlocking = blocking;
        
        if (blocking)
        {
            speed = 0;
        }
        else
        {
            speed = 2.5f;
        }
    }
}