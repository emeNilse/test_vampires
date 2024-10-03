using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CardTitle;
    [SerializeField] TextMeshProUGUI CardDescription;
    [SerializeField] Image CardIcon;
    [SerializeField] Button SelectButton;

    [SerializeField] UpgradeManager myUpgradeManager;

    UpgradeScriptableObjects myUpgrade;

    public void InitializeCard(UpgradeScriptableObjects anUpgrade)
    {
        myUpgrade = anUpgrade;
        PopulateUI();
    }

    public void SelectedUpgrade()
    {
        myUpgradeManager.ActivateUpgrade(myUpgrade);
    }

    void PopulateUI()
    {
        CardTitle.text = myUpgrade.name;
        CardIcon.sprite = myUpgrade.Icon;
        CardDescription.text = GenerateDescription();
    }

    string GenerateDescription()
    {
        string description = "";
        if (myUpgrade.attachPrefabs.Count > 0)
        {
            string AttachDescription = "";

            for (int i = 0; i < myUpgrade.attachPrefabs.Count; i++)
            { 
                GameObject p = myUpgrade.attachPrefabs[i];

                int check = 0;
                int duplicateAmount = 0;
                if (myUpgrade.attachPrefabs.Count > 1)
                {
                    while (check <  myUpgrade.attachPrefabs.Count - 1)
                    {
                        if (myUpgrade.attachPrefabs[i + check].name == myUpgrade.attachPrefabs[i].name)
                        {
                            duplicateAmount++;
                            i++;
                        }
                        check++;
                    }
                }

                if (duplicateAmount > 0)
                {
                    AttachDescription += "Add " + (duplicateAmount + 1).ToString() + " " + p.name + " that will ";
                }
                else
                {
                    AttachDescription += "Add a " + p.name + " that ";
                }

                AttachablePlayerUpgrade ap = p.GetComponent<AttachablePlayerUpgrade>();

                switch (ap.myBehaviour)
                {
                    case AttachablePlayerUpgrade.AttachedObjectBehaviour.PointToMouse:
                        AttachDescription += "point to where the player is aiming.";
                        break;
                    case AttachablePlayerUpgrade.AttachedObjectBehaviour.OrbitPlayer:
                        if (duplicateAmount > 1) //localization disaster
                        {
                            AttachDescription += "orbit around the player.";
                        }
                        else
                        {
                            AttachDescription += "orbits around the player.";
                        }
                        break;
                }

                AttachDescription += "<br>";
            }

            description += AttachDescription;
        }

        if (myUpgrade.WeaponSize > 0) description += GenerateStatDesc(myUpgrade.WeaponSize, "Weapon size");
        if (myUpgrade.Health > 0) description += GenerateStatDesc(myUpgrade.Health, "Health");
        if (myUpgrade.Speed > 0) description += GenerateStatDesc(myUpgrade.Speed, "Speed");
        if (myUpgrade.Damage > 0) description += GenerateStatDesc(myUpgrade.Damage, "Damage");
        if (myUpgrade.OrbitRadius > 0) description += GenerateStatDesc(myUpgrade.OrbitRadius, "Orbit Radius");

        return description;
    }

    string GenerateStatDesc(float someData, string aStat)
    {
        string returnString = "";
        if (someData ==  0) return returnString;

        if (someData > 0)
        {
            returnString += "Increase " + aStat + " by " + someData.ToString();
        }
        else
        {
            returnString += "Decrease " + aStat + " by " + someData.ToString();
        }

        return returnString;
    }
}
