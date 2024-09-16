

public class GameStateManager
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    { 
        get
        {
            if (_instance == null)
                _instance = new GameStateManager();
            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }
    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;

    private GameStateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;
        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }

    //if (GameStateManager.Instance.CurrentGameState != GameState.Gameplay) return; this went in shooter

    // all this went into playermovements
    //void Awake()
    //{
    //    GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    //}

//void OnDestroy()
//{
  //  GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
//}

//private void OnGameStateChanged(GameState newGameState)
//{
  //  enabled = newGameState == GameState.Gameplay;
//}

}
