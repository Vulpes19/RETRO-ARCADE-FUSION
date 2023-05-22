using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    void Start()
    {}

    //still work in progress
    public void ResumeGame()
    {
        SceneManager.LoadScene("scene1");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("scene1");
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
