using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : adaptiveAI
{
    public GameObject knifePrefab;
    public float throwCooldown = 2f;
    private bool canThrow = true;

    protected override void Start()
    {
        base.Start();
        maxHealth = 50f; // Set specific health for small enemy
        detectionRange = 5f; // Set specific detection range for small enemy
        damage = 10f; // Set specific attack damage for small enemy
    }

    protected override void OnPlayerDetected()
    {
        if (canThrow)
        {
            StartCoroutine(ThrowKnife());
        }
    }

    private IEnumerator ThrowKnife()
    {
        canThrow = false;
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion knifeRotation = Quaternion.LookRotation(direction);
        Vector3 spawnPosition = transform.position + direction * 1.5f;
        Instantiate(knifePrefab, spawnPosition, knifeRotation);
        yield return new WaitForSeconds(throwCooldown);
        canThrow = true;
    }
}