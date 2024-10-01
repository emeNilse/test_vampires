using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HighScoreDisplay;
    private string highScoreText;
    public string HighScoreText
    {
        get => highScoreText;
        private set
        {
            highScoreText = value;
            HighScoreDisplay.GetComponent<TMP_Text>().text = value;
        }
    }

    private void Start()
    {
        HighScoreText = $"HighScore Lvl: {PlayerPrefs.GetInt("HighScore", 1)}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
