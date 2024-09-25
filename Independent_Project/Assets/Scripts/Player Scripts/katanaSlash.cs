using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanaSlash : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Light attackFlashLight;
    public float flashDuration = 1f;
    private blockMechanic blockMechanic;


    public void Start()
    {
        blockMechanic = GetComponent<blockMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !blockMechanic.IsBlocking())
        {
            Slash();
        }
    }

    void Slash()
    {
        StartCoroutine(FlashLight());
        // Detect enemies in range of the attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("Slash attack hit!");
        }

        
    }

    IEnumerator FlashLight()
    {
        attackFlashLight.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        attackFlashLight.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}