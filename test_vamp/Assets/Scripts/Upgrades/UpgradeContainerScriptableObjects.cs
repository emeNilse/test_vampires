using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeContainerScriptableObject", menuName = "ScriptableObjects/Upgrade Container")]
public class UpgradeContainerScriptableObjects : ScriptableObject
{
    public List<UpgradeScriptableObjects> Upgrades = new List<UpgradeScriptableObjects>();
}
