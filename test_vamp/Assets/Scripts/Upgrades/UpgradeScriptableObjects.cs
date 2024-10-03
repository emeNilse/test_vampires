using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Profiling.LowLevel.Unsafe;

public class UpgradeAttribute : System.Attribute
{
    public string DisplayName;
}

[UpgradeAttribute(DisplayName = "Fool Baby!")]
[CreateAssetMenu(fileName = "UpgradeScriptableObject", menuName = "ScriptableObjects/Upgrade")]
public class UpgradeScriptableObjects : ScriptableObject
{
    [HideInInspector] public List<GameObject> attachPrefabs = new List<GameObject>();

    [HideInInspector] public float WeaponSize;
    [HideInInspector] public float Health;
    [HideInInspector] public float Speed;
    [HideInInspector] public float Damage;
    [HideInInspector] public float OrbitRadius;
    [HideInInspector] public Sprite Icon;
    [HideInInspector] public string Description;
}
