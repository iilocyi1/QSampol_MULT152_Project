using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : adaptiveAI
{
    public float attackRange = 1f;
    public int attackDamage = 20;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public Light attackFlashLight;
    public float flashDuration = 1f;
    public float attackCooldown = 1.5f;
    private bool canAttack = true;

    protected override void Start()
    {
        base.Start();
        maxHealth = 100f; // Set specific health for large enemy
        detectionRange = 4f; // Set specific detection range for large enemy
    }

    protected override void OnPlayerDetected()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && canAttack)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    private IEnumerator MeleeAttack()
    {
        canAttack = false;
        StartCoroutine(FlashLight());

        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        foreach (Collider player in hitPlayers)
        {
            player.GetComponent<Player>().TakeDamage(attackDamage);
            Debug.Log("Melee attack hit!");
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator FlashLight()
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