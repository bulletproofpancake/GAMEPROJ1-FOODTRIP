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
        UnPauseAudio();
        pauseMenuPanel.SetActive(false);
        isPaused = false;
        _gameManager.PauseGame(isPaused);
    }

    public void PauseTheGame()
    {
        PauseAudio();
        pauseMenuPanel.SetActive(true);
        isPaused = true;
        _gameManager.PauseGame(isPaused);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //Known bug: On 1 or 0 second of the core game loop,
    //this will not work because of how the end of summary will
    //take over this part immediately after being called.
    public void MainMenu()
    {
        MoneyManager.Instance.currentMoney = 0;
        StopAudio();
        SceneSelector.Instance.LoadNextScene("Main Menu_JK");
        ResumeTheGame();
    }

    void StopAudio()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("ArcadeBGM");
            FindObjectOfType<AudioManager>().Stop("VisualNovelBGM");
            FindObjectOfType<AudioManager>().Stop("OrderReady");
        }
    }

    void PauseAudio()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Pause("OrderReady");
        }
    }

    void UnPauseAudio()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().UnPause("OrderReady");
        }
    }
}
