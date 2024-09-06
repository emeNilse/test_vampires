using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//base script of all melee behaviours (to be placed on the prefab of a weapon that is melee)
public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
