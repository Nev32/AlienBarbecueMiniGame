using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    AudioSource buttonClickAudio;
    public AudioMixer audioMixer;
    public GameObject pauseMenu;
    static bool isPaused = false;
    public int sceneIndex;

    private void Awake()
    {
        buttonClickAudio = GetComponent<AudioSource>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sceneIndex == 1)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PlayGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        ResumeGame();
    }

    public void BackToMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicvolume", volume);
    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfxvolume", volume);
    }

    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }
}
