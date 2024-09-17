using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<EnemyStats> enemies = new List<EnemyStats>(); //enemy base
    

    public GameObject prefab;
    [SerializeField] Transform playerPosition;

    public float EnemyMeleeSpawnDistance = 0;
    public float EnemyMeleeSpawnrate = 0;
    public float NextEnemyMeleeSpawn = 0;
    public float EnemyRangedSpawnDistance = 0;
    public float EnemyRangedSpawnrate = 0;
    public float NextEnemyRangedSpawn = 0;

    public void EnemyUpdate()
    {
        if (NextEnemyMeleeSpawn >= 0.5)
        {
            SpawnMeleeRandom(); //Figure out the enemy spawn for both melee and ranged
            NextEnemyMeleeSpawn = 0;
        }
        else
        {
            NextEnemyMeleeSpawn += Time.deltaTime * EnemyMeleeSpawnrate;
        }

        if (NextEnemyRangedSpawn >= 0.5)
        {
            SpawnRangedRandom();
            NextEnemyRangedSpawn = 0;
        }
        else
        {
            NextEnemyRangedSpawn += Time.deltaTime * EnemyRangedSpawnrate;
        }


        //update bats
        foreach (MeleeEnemy enemy in enemies)
        {
            enemy.UpdateMeleeEnemy();
        }
        //update towers
        foreach (RangedEnemy enemy in enemies)
        {
            enemy.UpdateRangedEnemy();
        }
    }

    public void SpawnRandom()
    {
        Vector3 spawnPos = Random.insideUnitCircle.normalized;
        spawnPos *= EnemyMeleeSpawnDistance;
        SpawnEnemy(playerPosition.position + spawnPos);
    }

    public void SpawnEnemy(Vector3 aPosition)
    {
        GameObject e = Instantiate(prefab, aPosition, Quaternion.identity);
        e. = playerPosition; //figure out how to call the position of the player so that the enemy updates can track it
        enemies.Add(e);
        e.OnKilled.AddListener(EnemyKilled);
    }


    public void EnemyKilled(EnemyStats e)
    {
        e.OnKilled.RemoveAllListeners();

        if (enemies.Contains(e))
        {
            enemies.Remove(e);
        }

        Destroy(e.gameObject);
    }
}
