using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public void    updateScore(string brick)
    {
        if (brick == "one")
            score += 1;
        else if (brick == "three")
            score += 3;
        else if (brick == "five")
            score += 5;
        else if (brick == "seven")
            score += 7;
        scoreText.SetText("Score: " + score.ToString());
        if (score > highScore)
        {
            highScore = score;
            highScoreText.SetText( "High Score: " + highScore.ToString() );
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        if ( score >= 160)
            SceneManager.LoadScene("GameOver");
    }
}
