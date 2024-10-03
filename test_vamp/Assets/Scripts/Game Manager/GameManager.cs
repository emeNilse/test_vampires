using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : StateMachine
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null) Debug.LogError("[Enemymanager] Singleton already exists");
        Instance = this;
    }
    #endregion

    [SerializeField] XPBar myXPBar;


    private void Start()
    {
        SwitchState<PlayingState>();
    }

    private void Update()
    {
        UpdateStateMachine();
    }

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

}
