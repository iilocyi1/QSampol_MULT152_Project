using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float teleportDistance = 2f;
    public float invulnerabilityDuration = 0.5f;
    public float teleportCooldown = 5f; // Cooldown duration in seconds
    private bool isInvulnerable = false;
    private bool canTeleport = true; // Flag to check if teleport is available

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canTeleport)
        {
            // Using coroutine to handle the timing of the invulnerability and cooldown
            StartCoroutine(Dodge());
        }
    }

    private IEnumerator Dodge()
    {
        Vector3 teleportDirection = transform.forward * teleportDistance;
        Vector3 targetPosition = transform.position + teleportDirection;

        // Check if the target position is clear using a sphere cast
        if (!Physics.CheckSphere(targetPosition, 0.5f)) // Adjust the radius as needed
        {
            // Make the character invulnerable
            isInvulnerable = true;

            // Move the character
            transform.position = targetPosition;

            // Wait for the invulnerability duration
            yield return new WaitForSeconds(invulnerabilityDuration);

            // Make the character vulnerable again
            isInvulnerable = false;

            // Start the cooldown
            canTeleport = false;
            yield return new WaitForSeconds(teleportCooldown);
            canTeleport = true;
        }
        else
        {
            Debug.Log("Teleportation blocked by an obstacle.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isInvulnerable)
        {
            // Ignore collision
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            // Handle collision normally
        }
    }
}