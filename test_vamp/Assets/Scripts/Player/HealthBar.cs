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

    public void Awake()
    {
        currentHealth = characterData.MaxHealth;
        maxHealth = characterData.MaxHealth;

        healthBar.fillAmount = (float)currentHealth / (float)characterData.MaxHealth;

        UpdateHealthText();
    }

    public void LevelUpHealth()
    {
        maxHealth += healthIncrease; //Max health increase when level up
        currentHealth = maxHealth; //full health restored when level up
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        HealthText = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        UpdateHealthText() ;
        
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        OnDeath.Invoke();
        Debug.Log("Player is dead");
    }

    public void RestoreHealth(float amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            UpdateHealthText();

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                UpdateHealthText();
            }
        }

    }
}
