using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CloundGenScript : MonoBehaviour
{
    [SerializeField] List<GameObject> clouds;
    List<cloudMovement> cloudsList = new List<cloudMovement>();

    [SerializeField] float spawnRate = 0;
    [SerializeField] float nextCloudSpawn = 0;

    [SerializeField] GameObject endPoint;

    Vector3 startPos;


    void Start()
    {
        startPos = transform.position;
    }

   
    void Update()
    {
        if(nextCloudSpawn >= 0.5)
        {
            SpawnCloud();
            nextCloudSpawn = 0;
        }
        else
        {
            nextCloudSpawn += Time.deltaTime * spawnRate;
        }
    }

    void SpawnCloud()
    {
        int randCloud = Random.Range(0, clouds.Count);
        int randY = Random.Range(-1, 3);
        Vector3 cloudPos = new Vector3(startPos.x, startPos.y + randY, startPos.z);

        foreach(cloudMovement p in cloudsList)
        {
            if (!p.isActiveAndEnabled)
            {
                p.RespawnCloud(cloudPos);
                return;
            }
        }

        GameObject c = Instantiate(clouds[randCloud], cloudPos, Quaternion.identity);
        cloudsList.Add(c.GetComponent<cloudMovement>());
    }

    
}
