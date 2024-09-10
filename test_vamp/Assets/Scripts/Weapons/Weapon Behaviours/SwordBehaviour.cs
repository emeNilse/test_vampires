using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.parent.Rotate(-Vector3.forward, 360 * Time.deltaTime);
    }

}
