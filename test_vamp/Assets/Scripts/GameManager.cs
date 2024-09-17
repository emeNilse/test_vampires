using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null) Debug.LogError("[Enemymanager] Singleton already exists");
        Instance = this;
    }

    [SerializeField] EnemyManager myEnemyManager;
    [SerializeField] Player myPlayer;

    public enum GameState
    {
        PlayingState,
        PauseState
    }

    int State = 0;

    private void Update()
    {
        switch (State)
        {
            case 0:

                myPlayer.PlayerUpdate();
                myEnemyManager.EnemyUpdate();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    State = 1;
                }

                return;

            case 1:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    State = 0;
                }

                return;
        }
    }
}
