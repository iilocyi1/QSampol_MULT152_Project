using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    // Defining the teleportation method
    public float teleportDistance = 2f;
    public float invulnerabilityDuration = .5f;
    private bool isInvulnerable = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Using coroutine to handle the timing of the 
            //invulnerability
            StartCoroutine(Dodge());
        }
        
    }

    private IEnumerator Dodge()
    {
        //Make the character invulnerable
        isInvulnerable = true;

        //Move the character
        Vector3 teleportDirection = transform.forward * teleportDistance;
        transform.position += teleportDirection;

        //Wait for the invulnerability duration
        yield return new WaitForSeconds(invulnerabilityDuration);

        //Make the character vulnerable again
        isInvulnerable = false;
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
