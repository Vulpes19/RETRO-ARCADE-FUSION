using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip clip;
    
    public void StartGame()
    {
        AudioManager.instance.playSound(clip);
        SceneManager.LoadScene("GameMenu");
    }

    public void ExitGame()
    {
        AudioManager.instance.playSound(clip);
        Application.Quit();
    }
}
