using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    private SpriteFlash _spriteFlash; 
    //private SpriteRenderer _spriteRenderer;
    public string LevelText
    {
        get => levelText; 
        private set
        {
            levelText = value;
            LevelDisplay.GetComponent<TMP_Text>().text = value;
        }
    }
    public string HealthText
    {
        get => healthText;
        private set
        {
            healthText = value;
            HealthDisplay.GetComponent<TMP_Text>().text = value;
        }
    }
    [SerializeField] Image HealthBar;
    [SerializeField] Image XPBar;
    [SerializeField] GameObject LevelDisplay;
    [SerializeField] GameObject HealthDisplay;

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

    //add sword damage on level up
    public float extraDamage = 5.0f;
    public float addDamage = 0f;

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;

    public Color flashColor;
    public int numberOfFlashes;

    float invincibilityTimer;
    bool isInvincible;
    private string levelText;
    private string healthText;

    void Start()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteFlash = GetComponent<SpriteFlash>();

        // assign variables
        currentHealth = characterData.MaxHealth;
        maxHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;

        HealthBar.fillAmount = (float)currentHealth / (float)characterData.MaxHealth;
        XPBar.fillAmount = (float)experience / (float)experienceCap;


        LevelText = "Lvl: " + level.ToString();
        HealthText = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
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
            HealthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            UpdateText();
            addDamage += extraDamage;
        }
    }

    public void UpdateText()
    {
        LevelText = "Lvl: " + level.ToString();
        HealthText = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString(); 
    }

    void Update()
    {
        //Invincibility when taking damage
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }
    }

    

    public void takeDamage(float damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            HealthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            StartCoroutine(_spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
            UpdateText();

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
            UpdateText();

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                UpdateText();
            }
        }
        
    }
}
