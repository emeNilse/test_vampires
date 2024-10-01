using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class XPBar : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    public HealthBar healthBar;
    public UnityEvent OnLevelUp;

    [SerializeField] Image xPBar;
    [SerializeField] GameObject LevelDisplay;
    [SerializeField] TextMeshProUGUI HighScoreDisplay;

    public string LevelText
    {
        get => levelText;
        private set
        {
            levelText = value;
            LevelDisplay.GetComponent<TMP_Text>().text = value;
        }
    }
    public string HighScoreText
    {
        get => highScoreText;
        private set
        {
            highScoreText = value;
            HighScoreDisplay.GetComponent<TMP_Text>().text = value;
        }
    }

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;
    private string levelText;
    private string highScoreText;

    public void Awake()
    {
        xPBar.fillAmount = (float)experience / (float)experienceCap;
        UpdateXPText();
        UpdateHighScoreText();
    }
    
    public void UpdateXPText()
    {
        LevelText = "Lvl: " + level.ToString();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        xPBar.fillAmount = (float)experience / (float)experienceCap;
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            OnLevelUp.Invoke();
            LevelUp();
            healthBar.LevelUpHealth();
        }
    }

    public void LevelUp()
    {
        level++;
        experience -= experienceCap;
        experienceCap += experienceCapIncrease;
        xPBar.fillAmount = (float)experience / (float)experienceCap;
        UpdateXPText();
        CheckHighScore();
    }

    public void CheckHighScore()
    {
        if (level > PlayerPrefs.GetInt("HighScore", 1))
        {
            PlayerPrefs.SetInt("HighScore", level);
        }
    }

    public void UpdateHighScoreText()
    {
        HighScoreText = $"HighScore Lvl: {PlayerPrefs.GetInt("HighScore", 1)}";
    }
}
