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
    public float currentHealth;
    public float maxHealth;
    public float currentRecovery;
    public float currentMoveSpeed;
    public float currentMight;

    [Header("Experience/Level")]
    public bool didLevelUp = false;
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;
    public int healthIncrease = 10;
    public int speedIncrease = 1;
    public float mightIncrease = 2;
    public float recoveryIncrease = 3;

    //add sword damage on level up
    public float extraDamage = 5.0f;
    public float addDamage = 0f;

    //Damage taken
    private FloatingDamage _floatingDamage;

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;

    public Color flashColor;
    public int numberOfFlashes;

    float invincibilityTimer;
    bool isInvincible;
    private string levelText;
    private string healthText;

    void Awake()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteFlash = GetComponent<SpriteFlash>();

        _floatingDamage = GetComponent<FloatingDamage>();

        // assign variables
        currentHealth = characterData.MaxHealth;
        maxHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;

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
            LevelUp();
            didLevelUp = true;
        }
    }

    public void LevelUp()
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

    public void UpgradeMight()
    { 
        currentMight += mightIncrease;

        // Alternative method
        //UpgradeMenu.OnMightUpgrade.AddListener(UpgradeMight); //=>
        //{
        //  currentMight += might;
        //});
    }

    public void UpgradeRecovery()
    {
        currentRecovery += recoveryIncrease;
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
            string damage_text = damage.ToString();
            UpdateText();
            _floatingDamage.DamageFloat(damage_text);

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
