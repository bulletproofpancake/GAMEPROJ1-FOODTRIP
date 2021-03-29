using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameManager _gameManager;

    public static bool isPaused;

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        if (isPaused == false)
        {
            PauseTheGame();
        }
        else if (isPaused == true)
        {
            ResumeTheGame();
        }
    }

    public void ResumeTheGame()
    {
        //UnPauseAudio();
        pauseMenuPanel.SetActive(false);
        isPaused = false;
        _gameManager.PauseGame(isPaused);
    }

    public void PauseTheGame()
    {
        //PauseAudio();
        pauseMenuPanel.SetActive(true);
        isPaused = true;
        _gameManager.PauseGame(isPaused);
    }

    /*public void Options()
    {

    }*/

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneSelector.Instance.LoadNextScene("Main Menu_JK");
        ResumeTheGame();
    }

    //void PauseAudio()
    //{
    //    FindObjectOfType<AudioManager>().Pause("ParesSFX");
    //    FindObjectOfType<AudioManager>().Pause("KaninSFX");
    //}

    //void UnPauseAudio()
    //{
    //    FindObjectOfType<AudioManager>().UnPause("ParesSFX");
    //    FindObjectOfType<AudioManager>().UnPause("KaninSFX");
    //}
}
