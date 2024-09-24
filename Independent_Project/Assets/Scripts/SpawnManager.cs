using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private float yPosRange = 18;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 1.0f, 5.0f);

    }

    // Update is called once per frame
    void SpawnRandomEnemy()
    {
        float randXPos = Random.Range(37.0f, 48f);
        float randYPos = Random.Range(-yPosRange, yPosRange);
        int enemyPrefabIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 randPos = new Vector3(randXPos, 25, randYPos);
        Instantiate(enemyPrefabs[enemyPrefabIndex], randPos,
            enemyPrefabs[enemyPrefabIndex].transform.rotation);
    }
}
