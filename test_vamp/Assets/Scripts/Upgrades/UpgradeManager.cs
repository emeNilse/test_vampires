using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Player myPlayer;

    [SerializeField] UpgradeContainerScriptableObjects upgradeContainer;
    [SerializeField] GameObject UpgradeUI; //Upgrade canvas
    [SerializeField] UpgradeCard[] myUpgradeCards;
    [SerializeField] UpgradeScriptableObjects[] InitialUpgrades;

    public void Initialize(Player aPlayer)
    {
        HideUpgradeCards();
        myPlayer = aPlayer;
    }

    public void LevelUp()
    {
        GameManager.Instance.SwitchState<UpgradeState>();
        ShowUpgrades();
    }

    //when you pick an upgrade
    public void ActivateUpgrade(UpgradeScriptableObjects anUpgrade)
    {
        myPlayer.UpgradePlayer(anUpgrade); // an upgrade script for player
        GameManager.Instance.SwitchState<PlayingState>();
        UpgradeUI.SetActive(false);
        HideUpgradeCards();
    }

    public void ShowInitialUpgrades()
    {
        for (int i = 0; i < InitialUpgrades.Length; i++)
        {
            myUpgradeCards[i].transform.parent.gameObject.SetActive(true);
            myUpgradeCards[i].InitializeCard(InitialUpgrades[i]);
        }
        UpgradeUI.SetActive(true);
    }

    void ShowUpgrades()
    {
        int random = Random.Range(1, 4); //magic numbers?

        List<UpgradeScriptableObjects> randomUpgrades = new List<UpgradeScriptableObjects>();
        randomUpgrades.AddRange(upgradeContainer.Upgrades);

        for (int i = 0; i < random; i++)
        {
            myUpgradeCards[i].transform.parent.gameObject.SetActive(true);
            int randomUpgrade = Random.Range(0, randomUpgrades.Count);
            myUpgradeCards[i].InitializeCard(randomUpgrades[randomUpgrade]);
            randomUpgrades.RemoveAt(randomUpgrade);
        }

        UpgradeUI.SetActive(true);
    }

    void HideUpgradeCards()
    {
        foreach (UpgradeCard g in myUpgradeCards)
        { 
            g.transform.parent.gameObject.SetActive(false);
        }
    }
}
