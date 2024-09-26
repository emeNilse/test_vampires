using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<EnemyStats> enemies = new List<EnemyStats>(); //list of all enemies present
    

    public List<GameObject> enemyPrefabs;
    [SerializeField] Transform playerPosition;

    public float EnemySpawnDistance = 0;
    public float EnemySpawnrate = 0;
    public float NextEnemySpawn = 0;

    public void EnemyUpdate()
    {
        if (NextEnemySpawn >= 0.5)
        {
            SpawnEnemy();
            NextEnemySpawn = 0;
        }
        else
        {
            NextEnemySpawn += Time.deltaTime * EnemySpawnrate;
        }


        //update bats and towers
        foreach (EnemyStats enemy in enemies)
        {
            enemy.UpdateEnemy();
        }
    }

    public Vector3 SpawnRandom()
    {
        Vector3 spawnPos = Random.insideUnitCircle.normalized;
        spawnPos *= EnemySpawnDistance;
        spawnPos += playerPosition.position;
        return spawnPos;
    }

    public void SpawnEnemy()
    {
        Vector3 aPosition = SpawnRandom();
        int randEnemy = Random.Range(0, enemyPrefabs.Count);
        GameObject e = Instantiate(enemyPrefabs[randEnemy], aPosition, Quaternion.identity);
        e.GetComponent<EnemyStats>().findplayer = playerPosition; 
        enemies.Add(e.GetComponent<EnemyStats>());
        e.GetComponent<EnemyStats>().OnKilled.AddListener(EnemyKilled);
    }


    public void EnemyKilled(EnemyStats e)
    {
        e.OnKilled.RemoveAllListeners();

        if (enemies.Contains(e))
        {
            enemies.Remove(e);
        }

        //Destroy(e.gameObject);
    }
}
