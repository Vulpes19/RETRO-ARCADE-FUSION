using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public bool isPaused;
    private int timer = 3;
    private IEnumerator coroutine;
    
    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            if (isPaused)
                StartCoroutine(ResumeGame());
            else
                PauseGame();
        }
    }

    IEnumerator ResumeGame()
    {
        while (timer > 0)
        {
            Debug.Log(timer.ToString());
            yield return new WaitForSecondsRealtime(1);
            timer--;
        }
        isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void PauseGame()
    {
        isPaused = true;
        menu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void retryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(ResumeGame());
    }

    void quitGame()
    {
        SceneManager.LoadScene("MainMenu");
        StartCoroutine(ResumeGame());
    }
}
