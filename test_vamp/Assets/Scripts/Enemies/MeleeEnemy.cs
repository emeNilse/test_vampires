using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemy : EnemyStats
{
    //public EnemyScriptableObject enemyData;
    //public Transform player = findplayer;
    public float lineOfSight;
    private bool markedPlayer;
    

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    
    public override void UpdateEnemy()
    {
        
        float distanceFromPlayer = Vector2.Distance(findplayer.position, transform.position);
        if (distanceFromPlayer <= lineOfSight && !markedPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, findplayer.position, enemyData.MoveSpeed * Time.deltaTime);
            markedPlayer = true;
        }
        else if (markedPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, findplayer.position, enemyData.MoveSpeed * Time.deltaTime);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
