using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool soundOn = true;
    public GameObject menu;
    public bool isPaused = false;
    private int timer = 3;
    private IEnumerator coroutine;
    public TextMeshProUGUI countdown;
    public AudioClip pauseSound;
    public AudioClip buttonSound;

    void Start()
    {
        countdown.gameObject.SetActive(false);
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
        timer = 3;
        countdown.gameObject.SetActive(true);
        GameObject icon1 = GameObject.Find("exit");
        GameObject icon2 = GameObject.Find("sound");
        GameObject icon3 = GameObject.Find("retry");
        icon1.SetActive(false);
        icon2.SetActive(false);
        icon3.SetActive(false);
        while (timer > 0)
        {
            countdown.SetText(timer.ToString());
            yield return new WaitForSecondsRealtime(1);
            timer--;
        }
        countdown.gameObject.SetActive(false);
        isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1.0f;
        icon1.SetActive(true);
        icon2.SetActive(true);
        icon3.SetActive(true);
    }

    public void PauseGame()
    {
        AudioManager.instance.playSound(pauseSound);
        //pauseAudio.Play();
        isPaused = true;
        menu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void retryGame()
    {
        if (soundOn == true)
            AudioManager.instance.playSound(buttonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
        menu.SetActive(false);
        countdown.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        //StartCoroutine(ResumeGame());
    }
    public void sound()
    {
        AudioManager.instance.toggleSound();
    }
    public void quitGame()
    {
        if ( soundOn == true )
            AudioManager.instance.playSound(buttonSound);
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
        menu.SetActive(false);
        countdown.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
