using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    [SerializeField] EnemyManager myEnemyManager;
    [SerializeField] Player myPlayer;
    [SerializeField] XPBar myXPBar;
    [SerializeField] HealthBar myHealthBar;
    [SerializeField] GameObject myPauseCanvas;
    [SerializeField] GameObject myUpgradeCanvas;
    [SerializeField] GameObject myGameOverCanvas;

    public override void UpdateState()
    {
        base.UpdateState();

        myPauseCanvas.SetActive(false);
        myUpgradeCanvas.SetActive(false);
        myGameOverCanvas.SetActive(false);
        myPlayer.PlayerUpdate();
        myEnemyManager.EnemyUpdate();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.SwitchState<PauseState>();
        }
        myXPBar.OnLevelUp.AddListener(PlayerLevelUp);
        void PlayerLevelUp()
        {
            GameManager.Instance.SwitchState<UpgradeState>();
        }
        myHealthBar.OnDeath.AddListener(PlayerIsDead);
        void PlayerIsDead()
        {
            GameManager.Instance.SwitchState<GameOverState>();
        }
    }
}
