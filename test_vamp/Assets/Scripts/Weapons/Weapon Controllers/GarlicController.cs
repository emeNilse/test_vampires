using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    
    protected override void Start()
    {
        base.Start();
    }

    
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(prefab);
        spawnedGarlic.transform.position = transform.position; //assign the position to be the same as this object which is parented to the player
        spawnedGarlic.transform.parent = transform; //so that it spawns below this object
    }
}
