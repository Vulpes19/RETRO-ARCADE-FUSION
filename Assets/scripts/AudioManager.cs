using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    private bool isSoundOn = true;
    public AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void findAudioManager()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach ( GameObject rootObject in rootObjects )
        {
            AudioSource audioSource = rootObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                Debug.Log(audioSource.name);
                Debug.Log(SceneManager.GetActiveScene().name);
                source = audioSource;
                Debug.Log("ITS UPDATED");
                break;
            }
        }

    }
    public void playSound(AudioClip clip)
    {
            findAudioManager();
        if (instance != null)
            Debug.Log("instance is good");
        if (source == null)
            Debug.Log("source is null");
        if (isSoundOn && clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
    }
    public void toggleSound()
    {
        isSoundOn = !isSoundOn;
    }
}
