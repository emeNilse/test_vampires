using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    [SerializeField] Image HealthBar;
    [SerializeField] Image XPBar;
    [SerializeField] Text Level;

    //current stats
    float currentHealth;
    float maxHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;
    public int healthIncrease = 10;
    public int speedIncrease = 1;
    public int mightIncrease = 2;

    void Awake()
    {
        // assign variables
        currentHealth = characterData.MaxHealth;
        maxHealth = characterData.MaxHealth;
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
            maxHealth += healthIncrease; //Max health increase when level up
            currentHealth = maxHealth; //full health restored when level up
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
            HealthBar.fillAmount = (float)currentHealth / (float)maxHealth;
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
        if(currentHealth < maxHealth)
        {
            currentHealth += amount;
            HealthBar.fillAmount = (float)currentHealth / (float)maxHealth;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        
    }
}
