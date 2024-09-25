using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCollisions : MonoBehaviour
{
    public int attackDamage = 20;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with " + other.name);
        // Check if the other object has an Enemy component
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Enemy hit!");
            // Apply damage to the enemy
            enemy.TakeDamage(attackDamage);
        }

        Destroy(gameObject);


    }
}

