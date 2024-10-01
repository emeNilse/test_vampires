using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemy : EnemyStats
{
    public float lineOfSight;
    private bool markedPlayer;
    //Animator am;
    

    void Start()
    {
        //am = GetComponent<Animator>();
    }

    
    public override void UpdateEnemy()
    {
        //am.SetTrigger("TriggerAnimation"); 
        //this stops the enemy animation, but it is delayed by one loop when pause is activated
        //am.SetBool("BatMove", false);

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

    public override void Despawn()
    {
        base.Despawn();
        Effects.SpawnDeathFireWorkFX(transform.position);
    }

    //Not to be called
    public override void Dead()
    {
        OnKilled.Invoke(this);
        Effects.SpawnDeathFireWorkFX(transform.position);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
