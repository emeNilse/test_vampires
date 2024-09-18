using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    [Header("Weapon Stats")]
    public WeaponsScriptableObjects weaponData;
    float currentCooldown = 0;

    public void SwordUpdate()
    {
        currentCooldown -= Time.deltaTime; //cooldown is zero at start
        if (currentCooldown <= 0 &&  Input.GetKeyDown(KeyCode.Space))
        {
            GameObject spawnedSword = Instantiate(weaponData.Prefab);
            spawnedSword.transform.position = new Vector3(transform.parent.position.x + -0.02f, transform.parent.position.y + 1.61f, transform.parent.position.z + 0); //assign the position to be the same as this object which is parented to the player
            spawnedSword.transform.parent = transform; //so that it spawns below this object
            currentCooldown = weaponData.CooldownDuration; //set cooldown
        }
    }

}
