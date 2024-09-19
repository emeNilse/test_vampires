using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public UnityEvent<EnemyStats> OnKilled;
    public Transform findplayer;
    [SerializeField] GameObject explode;

    //current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public virtual void UpdateEnemy()
    {

    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        OnKilled.Invoke(this);
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.takeDamage(currentDamage); //make sure to use currentDamage in case of damage multipliers
        }
    }
}
