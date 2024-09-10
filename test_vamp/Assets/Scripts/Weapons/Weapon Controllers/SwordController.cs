using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    

   // private RaycastHit2D[] hits;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSword = Instantiate(weaponData.Prefab);
        spawnedSword.transform.position = new Vector3(-0.02f, 1.61f, 0); //assign the position to be the same as this object which is parented to the player
        spawnedSword.transform.parent = transform; //so that it spawns below this object
    }

}
