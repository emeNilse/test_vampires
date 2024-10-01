using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public UnityEvent<EnemyStats> OnKilled;
    public Transform findplayer;

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

    public void Initialize(Vector3 aPostion)
    {
        gameObject.transform.position = aPostion;
    }

    public virtual void Respawn(Vector3 aPosition)
    {
        Initialize(aPosition);
        currentHealth = enemyData.MaxHealth;
        gameObject.SetActive(true);
    }

    public virtual void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            //Dead();
        }
    }
    
    public bool IsAlive()
    {
        if (currentHealth > 0)
        { 
            return true;
        }
        return false;
    }

    public virtual void Dead()
    {
        
    }

    public float GetDamage()
    {
        int level = GameManager.Instance.GetLevel();
        float damage = currentDamage * (1 + (level / 4));
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.takeDamage(GetDamage()); //make sure to use currentDamage in case of damage multipliers
        }
    }
}
