using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<EnemyStats> enemies = new List<EnemyStats>(); //list of all enemies present
    

    public List<GameObject> enemyPrefabs;
    [SerializeField] Transform playerPosition;
    [SerializeField] GameObject itemPrefab;

    public float EnemySpawnDistance = 0;
    public float EnemySpawnrate = 1;
    public float NextEnemySpawn = 0;
    public float EnemySpawnIncreaseBase = 1;
    public float EnemySpawnIncreasePercentage = 0.1f;

    public void EnemyUpdate()
    {
        int Level = GameManager.Instance.GetLevel();

        if (NextEnemySpawn >= EnemySpawnrate)
        {
            SpawnEnemy();
            NextEnemySpawn = 0;
        }
        else
        {
            NextEnemySpawn += Time.deltaTime * (EnemySpawnIncreaseBase + ((Level - 1) * EnemySpawnIncreasePercentage));
        }


        //update bats and towers
        foreach (EnemyStats enemy in enemies)
        {
            if(!enemy.IsAlive() && enemy.isActiveAndEnabled)
            {
                enemy.Despawn();
                //SpawnXPOrb(enemy.transform.position);
                continue;
            }
            enemy.UpdateEnemy();
        }
    }

    public void SpawnXPOrb(Vector3 aPosition)
    {
        Instantiate(itemPrefab, aPosition, Quaternion.identity);
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

        GameObject EnemyToSpawn = enemyPrefabs[randEnemy];

        foreach (EnemyStats enemy in enemies)
        { 
            if (!enemy.isActiveAndEnabled)
            {
                enemy.Respawn(aPosition);
                return;
            }
        }

        GameObject e = Instantiate(EnemyToSpawn, aPosition, Quaternion.identity);
        e.GetComponent<EnemyStats>().findplayer = playerPosition; //Why this works kind of drives me insane. Why can't findplayer be assigned to enemy prefabs?
        e.GetComponent<EnemyStats>().Initialize(aPosition); //This forces enemy "Start" functions to run, because the standard Start function wouldn't run when enemy spawned
        enemies.Add(e.GetComponent<EnemyStats>());

        //e.GetComponent<EnemyStats>().OnKilled.AddListener(EnemyKilled); No longer needed, but want to keep as reference
    }

    //This is no longer called as enemies are not destroyed, but I want to keep it as a reference
    public void EnemyKilled(EnemyStats e)
    {
        e.OnKilled.RemoveAllListeners();

        if (enemies.Contains(e))
        {
            enemies.Remove(e);
        }
    }
}
