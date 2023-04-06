using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    int score = 0;
    int highScore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        scoreText.SetText( "Score: " + score.ToString() );
        highScoreText.SetText( "High Score: " + highScore.ToString() );
    }

    public void    updateScore()
    {
        score += 1;
        scoreText.SetText("Score: " + score.ToString());
        if (score > highScore)
        {
            highScore = score;
            highScoreText.SetText( "High Score: " + highScore.ToString() );
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
