using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Not being used. This was just the first spawner I created before having Enemy Manager take care of spawning
    
    [SerializeField] GameObject swarmPrefab;
    [SerializeField] GameObject shooterPrefab;

    [SerializeField] float swarmInterval;
    [SerializeField] float shooterInterval;

    void Start()
    {
        StartCoroutine(spawnEnemny(swarmInterval, swarmPrefab));
        StartCoroutine(spawnEnemny(shooterInterval, shooterPrefab));
    }

    
    private IEnumerator spawnEnemny(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemny(interval, enemy));
    }

}
