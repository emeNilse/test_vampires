using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedEnemy : EnemyStats
{
    //Attack/Action variables
    public float lineOfSight;
    public float shootingRange;
    public float runAway;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;

    //Speech variables
    private ShooterSpeaks _shooterSpeaks;
    private float timeToSpeak;
    private float speakRate = 5f;
    private float chanceToSpeak;

    void Start()
    {
        _shooterSpeaks = GetComponent<ShooterSpeaks>();
        
    }


    public override void UpdateEnemy()
    {
        Speaking();

        float distanceFromPlayer = Vector2.Distance(findplayer.position, transform.position);
        //transform.RotateAround(player.position, Vector3.forward, 20 * Time.deltaTime); Attempt at rotation around the player
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, findplayer.position, enemyData.MoveSpeed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            // make code so bullet spawns "outside" of shooter
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        else if(distanceFromPlayer <= runAway)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, findplayer.position, -enemyData.MoveSpeed * Time.deltaTime);
        }
    }

    public void Speaking()
    {
        if (timeToSpeak < Time.time)
        {
            chanceToSpeak = Random.Range(0, 10);
            if (chanceToSpeak >= 5 && _shooterSpeaks != null)
            {
                _shooterSpeaks.StartSpeak();
            }
            timeToSpeak = Time.time + speakRate;
        }
    }

    public override void Dead()
    {
        OnKilled.Invoke(this);
        Effects.SpawnDeathExplosionFX(transform.position);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, runAway);
    }
}
