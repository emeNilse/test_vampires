using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedEnemy : EnemyStats
{
    public float lineOfSight;
    public float shootingRange;
    public float runAway;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void UpdateEnemy()
    {
       

        float distanceFromPlayer = Vector2.Distance(findplayer.position, transform.position);
        //transform.RotateAround(player.position, Vector3.forward, 20 * Time.deltaTime);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, findplayer.position, enemyData.MoveSpeed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            //Attempt at rotation around the player
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        else if(distanceFromPlayer <= runAway)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, findplayer.position, -enemyData.MoveSpeed * Time.deltaTime);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, runAway);
    }
}
