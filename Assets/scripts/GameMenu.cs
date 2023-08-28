using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public AudioClip clip;
    public void loadBreakout()
    {
        AudioManager.instance.playSound(clip);
        SceneManager.LoadScene("Breakout");
    }
    public void loadPong()
    {
        AudioManager.instance.playSound(clip);
        SceneManager.LoadScene("Pong");
    }
}
