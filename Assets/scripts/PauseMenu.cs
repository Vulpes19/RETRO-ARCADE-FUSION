using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public bool isPaused;
    
    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        isPaused = true;
        menu.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
