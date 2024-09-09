using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmPrefab;

    [SerializeField]
    private float swarmInterval = 3.5f;

    void Start()
    {
        StartCoroutine(spawnEnemny(swarmInterval, swarmPrefab));
    }

    
    private IEnumerator spawnEnemny(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemny(interval, enemy));
    }
}
