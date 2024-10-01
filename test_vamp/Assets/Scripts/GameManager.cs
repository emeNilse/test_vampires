using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null) Debug.LogError("[Enemymanager] Singleton already exists");
        Instance = this;
        UpgradeMenu upgradePanel = myUpgradeCanvas.GetComponentInChildren<UpgradeMenu>();
        upgradePanel.OnUpgrade.AddListener(UpgradeChosen);
        void UpgradeChosen()
        {
            State = 0;
        }
    }
    #endregion

    [SerializeField] EnemyManager myEnemyManager;
    [SerializeField] Player myPlayer;
    [SerializeField] XPBar myXPBar;
    [SerializeField] HealthBar myHealthBar;
    [SerializeField] GameObject myPauseCanvas;
    [SerializeField] GameObject myUpgradeCanvas;
    [SerializeField] GameObject myGameOverCanvas;

    public int GetLevel()
    {
        return myXPBar.level;
    }

    public enum GameState
    {
        PlayingState,
        PauseState,
        UpgradeState,
        GameOverState
    }

    int State = 0;

    private void Update()
    {
        switch (State)
        {
            case 0:
                //Time.timeScale = 1; // I know I shouldn't but I'm frustrated
                myPauseCanvas.SetActive(false);
                myUpgradeCanvas.SetActive(false);
                myGameOverCanvas.SetActive(false);
                myPlayer.PlayerUpdate();
                myEnemyManager.EnemyUpdate();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    State = 1;
                }
                myXPBar.OnLevelUp.AddListener(PlayerLevelUp);
                void PlayerLevelUp()
                {
                    State = 2;
                }
                myHealthBar.OnDeath.AddListener(PlayerIsDead);
                void PlayerIsDead()
                {
                    State = 3;
                }
                return;

            case 1:
                //Time.timeScale = 0; // I know I shouldn't but I'm frustrated
                myPauseCanvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    State = 0;
                }
                return;

            case 2:
                myUpgradeCanvas.SetActive(true);
                return;

            case 3:
                myGameOverCanvas.SetActive(true);
                return;
        }
    }
}
