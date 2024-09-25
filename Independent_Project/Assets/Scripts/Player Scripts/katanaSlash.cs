using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanaSlash : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Slash();
        }
    }

    void Slash()
    {
        // Detect enemies in range of the attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        // Optionally, add visual or sound effects here
        Debug.Log("Slash attack performed!");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}