using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    public HealthBar myHealthBar;
    public XPBar myXPBar;

    //Damage taken sprites
    private FloatingDamage _floatingDamage;

    //Invincibility flash
    private SpriteFlash _spriteFlash;

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;

    public Color flashColor;
    public int numberOfFlashes;

    float invincibilityTimer;
    bool isInvincible;

    //current stats
    public float currentRecovery;
    public float currentMoveSpeed;
    public float currentMight;

    public int speedIncrease = 1;
    public float mightIncrease = 1;
    public float recoveryIncrease = 3;

    //add sword damage on level up
    public float extraDamage = 5.0f;
    public float addDamage = 0f;

    void Awake()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteFlash = GetComponent<SpriteFlash>();

        _floatingDamage = GetComponent<FloatingDamage>();

        // assign variables
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;

        myHealthBar.UpdateHealthText();
        myXPBar.UpdateXPText();
        myXPBar.UpdateHighScoreText();
    }

    //Invincibility when taking damage
    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }

    public void takeDamage(float damage)
    {
        if(!isInvincible)
        {
            myHealthBar.TakeDamage(damage);

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            StartCoroutine(_spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
            string damage_text = damage.ToString();
            _floatingDamage.DamageFloat(damage_text);
        }
    }

    //Health potion Collectibles
    public void RestoreHealth(float amount)
    {
        myHealthBar.RestoreHealth(amount);
    }
    //XP Points Collectibles
    public void IncreaseExperience(int amount)
    {
        myXPBar.IncreaseExperience(amount);
    }

    public void LevelUp()
    {
        // how to connect this, this won't be called anymore
        addDamage += extraDamage;
    }

    //Called on Upgrade
    public void UpgradeMight()
    {
        currentMight += mightIncrease;
    }
    //Called on Upgrade
    public void UpgradeRecovery()
    {
        currentRecovery += recoveryIncrease;
    }
}
