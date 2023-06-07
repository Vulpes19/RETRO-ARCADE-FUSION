using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PongScoreManager : MonoBehaviour
{
    public static PongScoreManager instance;

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI AIText;
    private int playerScore = 0;
    private int AIScore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerText.SetText(playerScore.ToString());
        AIText.SetText(AIScore.ToString());
    }

    public void updateScore(string paddle)
    {
        if (paddle == "player")
            playerScore += 1;
        if (paddle == "AI")
            AIScore += 1;
        playerText.SetText(playerScore.ToString());
        AIText.SetText(AIScore.ToString());
        if (AIScore == 10)
            SceneManager.LoadScene("GameOver");
        if (playerScore == 10)
            SceneManager.LoadScene("GameOver");
    }
    public int getPlayerScore()
    {
        return (playerScore);
    }
    public int getAIScore()
    {
        return (AIScore);
    }
}
