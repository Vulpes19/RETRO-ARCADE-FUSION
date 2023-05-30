using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void loadBreakout()
    {
        SceneManager.LoadScene("Breakout");
    }
    public void loadPong()
    {
        SceneManager.LoadScene("Pong");
    }
}
