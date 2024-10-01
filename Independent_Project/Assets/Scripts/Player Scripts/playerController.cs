using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float jumpHeight = 2.0f;
    public float speed = 2.0f;
    private float lrInput;
    private float udInput;
    private float jumpInput;
    public GameObject projectilePrefab;
    private bool isGrounded;
    private bool isBlocking = false;
    private Rigidbody rb;
    public float fallMultiplier = 2.5f; // Multiplier to increase fall speed
    private int projectileCount = 0; // Counter for projectiles fired
    private bool canFire = true; // Flag to check if firing is allowed
    public float fireCooldown = 2.5f; // Cooldown duration in seconds

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Vector3 movementDirection = new Vector3(-udInput, 0, lrInput).normalized;

            // Rotate the character based on movement direction
            if (movementDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movementDirection);
            }

            // Translate the character in the direction it is facing
            Vector3 movement = movementDirection * Time.fixedDeltaTime * speed;
            transform.Translate(movement, Space.World);

            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

            // Handle jumping
            if (isGrounded && jumpInput > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude), rb.velocity.z);
            }

            // Apply fall multiplier for quicker fall
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

            // Instantiate projectile
            if (Input.GetKeyDown(KeyCode.G) && canFire)
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
                    Instantiate(projectilePrefab, spawnPosition, transform.rotation);
                }

                projectileCount++;

                if (projectileCount >= 3)
                {
                    StartCoroutine(FireCooldown());
                }
            }
        }
    }

    private IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
        projectileCount = 0; // Reset the counter after cooldown
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
            speed = 2.0f;
        }
    }
}