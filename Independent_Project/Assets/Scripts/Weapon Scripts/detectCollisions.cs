using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}

