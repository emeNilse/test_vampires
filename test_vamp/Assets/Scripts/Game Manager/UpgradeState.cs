using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeState : State
{
    [SerializeField] GameObject myUpgradeCanvas;

    private void Awake()
    {
        UpgradeMenu upgradePanel = myUpgradeCanvas.GetComponentInChildren<UpgradeMenu>();
        upgradePanel.OnUpgrade.AddListener(UpgradeChosen);
        void UpgradeChosen()
        {
            GameManager.Instance.SwitchState<PlayingState>();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        myUpgradeCanvas.SetActive(true);
    }
}
