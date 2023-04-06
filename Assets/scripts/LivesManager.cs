using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        livesText.SetText(lives.ToString());
    }


    public void loseLife()
    {
        lives -= 1;
        livesText.SetText(lives.ToString());
    }
}
