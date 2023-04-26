using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject exitButton;
    public GameObject retryButton;
    public GameObject settingsButton;
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
        exitButton.SetActive(false);
        retryButton.SetActive(false);
        settingsButton.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        isPaused = true;
        menu.SetActive(true);
        exitButton.SetActive(true);
        retryButton.SetActive(true);
        settingsButton.SetActive(true);
        Button b1 = exitButton.GetComponent<Button>();
        Button b2 = retryButton.GetComponent<Button>();
        Button b3 = settingsButton.GetComponent<Button>();
        b1.onClick.AddListener(quitGame);
        b2.onClick.AddListener(retryGame);
        Time.timeScale = 0.0f;
    }

    public void retryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
