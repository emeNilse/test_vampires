using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    [SerializeField] Image HealthBar;
    [SerializeField] Image XPBar;

    //current stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    void Awake()
    {
        // assign variables
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        HealthBar.fillAmount = (float)currentHealth / (float)characterData.MaxHealth;
        XPBar.fillAmount = (float)experience / (float)experienceCap;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        XPBar.fillAmount = (float)experience / (float)experienceCap;
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
            XPBar.fillAmount = (float)experience / (float)experienceCap;
        }
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }
    }

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public void takeDamage(float damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            HealthBar.fillAmount = (float)currentHealth / (float)characterData.MaxHealth;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        Debug.Log("Player is dead");
    }

    public void RestoreHealth(float amount)
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            HealthBar.fillAmount = (float)currentHealth / (float)characterData.MaxHealth;

            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }
}
