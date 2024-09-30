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
            myPlayer.didLevelUp = false;
            State = 0;
        }
    }
    #endregion

    [SerializeField] EnemyManager myEnemyManager;
    [SerializeField] Player myPlayer;
    [SerializeField] GameObject myPauseCanvas;
    [SerializeField] GameObject myUpgradeCanvas;

    public enum GameState
    {
        PlayingState,
        PauseState,
        UpgradeState
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
                myPlayer.PlayerUpdate();
                myEnemyManager.EnemyUpdate();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    State = 1;
                }
                else if(myPlayer.didLevelUp)
                {
                    State = 2;
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

        }
    }
}
