using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    // Defining the teleportation method
    public float teleportDistance = 2f;
    public float invulnerabilityDuration = .5f;
    private bool isInvulnerable = false;
    public float teleportCooldwon = 5f;
    private bool canTeleport = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canTeleport)
        {
            // Using coroutine to handle the timing of the 
            //invulnerability
            StartCoroutine(Dodge());
        }

    }

    private IEnumerator Dodge()
    {
        Vector3 teleportDirection = transform.forward * teleportDistance;
        Vector3 targetPosition = transform.position + teleportDirection;

        //Check if the target position is clear
        //Character seems to fall in between objects sometimes
        if (!Physics.Raycast(transform.position, teleportDirection, teleportDistance))
        {

            //Make the character invulnerable
            isInvulnerable = true;

            //Move the character
            transform.position = targetPosition;

            //Wait for the invulnerability duration
            yield return new WaitForSeconds(invulnerabilityDuration);

            //Make the character vulnerable again
            isInvulnerable = false;

            canTeleport = false;
            yield return new WaitForSeconds(teleportCooldwon);
            canTeleport = true;
        }
        else
        {
            Debug.Log("Teleportation blocked by an obstacle.");

        }
    }

        //Setting up parameters for and if else for isInvulnerable
    private void OnCollisionEnter(Collision collision)
        {
            if (isInvulnerable)
            {
                //Ignore collision
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
            else
            {
                //Handle collision normally
            }


        }
    
}
