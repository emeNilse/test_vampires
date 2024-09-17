using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemy : EnemyStats
{
    public UnityEvent<MeleeEnemy> OnKilled;
    //public EnemyScriptableObject enemyData;
    public Transform player;
    public float lineOfSight;
    private bool markedPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    public void UpdateMeleeEnemy()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= lineOfSight && !markedPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, enemyData.MoveSpeed * Time.deltaTime);
            markedPlayer = true;
        }
        else if (markedPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, enemyData.MoveSpeed * Time.deltaTime);
        }
    }

    public override void Dead()
    {
        OnKilled.Invoke(this);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
