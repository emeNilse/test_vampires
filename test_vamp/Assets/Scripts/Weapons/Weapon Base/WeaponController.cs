using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic script for all weapon controllers

public class WeaponController : MonoBehaviour
{

    [Header("Weapon Stats")]
    public WeaponsScriptableObjects weaponData;
    float currentCooldown;

    protected Player pm;

    protected virtual void Start()
    {
        pm = FindObjectOfType<Player>();
        currentCooldown = weaponData.CooldownDuration; //at the start set the weapon cooldown to be the cooldown duration
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f) // Once the cooldown becomes 0, attack
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
