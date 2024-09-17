using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;
   
    void Start()
    {
        player = FindObjectOfType<Player>().transform;

        // player = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.MoveSpeed * Time.deltaTime); //constantly move enemy towards player
        
        // a different version of moving towards the player
        //distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.forward;
    }
}
