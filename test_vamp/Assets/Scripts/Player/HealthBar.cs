using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class HealthBar : MonoBehaviour
{
    public string HealthText
    {
        get => healthText;
        private set
        {
            healthText = value;
            HealthDisplay.GetComponent<TMP_Text>().text = value;
        }
    }

    public UnityEvent OnDeath;
    public CharacterScriptableObject characterData;

    [SerializeField] Image healthBar;
    [SerializeField] GameObject HealthDisplay;

    private string healthText;
    public float currentHealth;
    public float maxHealth;
    public int healthIncrease = 10;
    private float recoveryTimer = 2f;
    private float currentRecovery;
    private float recoveryIncrease = 1;

    public void Awake()
    {
        currentHealth = characterData.MaxHealth;
        maxHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        UpdateHealthText();
    }

    public void Update()
    {
        recoveryTimer -= Time.deltaTime;
        if (currentHealth < maxHealth && recoveryTimer <= 0)
        {
            currentHealth += currentRecovery;
            UpdateHealthFill();
            UpdateHealthText();
            recoveryTimer = 2f;
        }
    }

    public void LevelUpHealth()
    {
        maxHealth += healthIncrease; //Max health increase when level up
        currentHealth = maxHealth; //full health restored when level up
        UpdateHealthFill();
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        HealthText = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }
    public void UpdateHealthFill()
    {
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthFill();
        UpdateHealthText();
        
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        OnDeath.Invoke();
        //Effects.SpawnPlayerBloodFX(transform.position);
        Debug.Log("Player is dead");
    }

    public void RestoreHealth(float amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            UpdateHealthFill();
            UpdateHealthText();

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                UpdateHealthText();
            }
        }
    }
    
    //Call on Upgrade
    public void UpgradeRecovery()
    {
        currentRecovery += recoveryIncrease;
    }
}
