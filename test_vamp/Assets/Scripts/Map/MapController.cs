using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChuncks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    playermovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChucnks;
    GameObject latestChunk;
    public float maxOpDist; // must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizationCooldownDur;
    
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<playermovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunckChecker();
        ChunkOptimizer();
    }

    void ChunckChecker()
    {
        if(!currentChunk)
        {
            return;
        }
        
        if(pm.moveDir.x > 0 &&  pm.moveDir.y == 0) // right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) // left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) // up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0) // down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) // right up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) // right down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) // left up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                spawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) // left down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                spawnChunk();
            }
        }
    }

    void spawnChunk()
    {
        int rand = Random.Range(0, terrainChuncks.Count);
        latestChunk = Instantiate(terrainChuncks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChucnks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0)
        {
            optimizerCooldown = optimizationCooldownDur;
        }
        else
        {
            return;
        }
        
        foreach(GameObject chunk in spawnedChucnks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            { 
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
