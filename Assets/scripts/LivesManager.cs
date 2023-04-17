using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;

    public int lives = 3;
    public TextMeshProUGUI livesText;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        livesText.SetText("Lives: " + lives.ToString());
    }


    public void loseLife()
    {
        lives -= 1;
        if (lives >= 0)
            SceneManager.LoadScene("GameOver");
        livesText.SetText("Lives: " + lives.ToString());
    }
}
