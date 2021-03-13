using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    public static bool isPaused;

    public void PauseGame()
    {
        if (isPaused == false)
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            FindObjectOfType<AudioManager>().Pause("ParesSFX");
            FindObjectOfType<AudioManager>().Pause("KaninSFX");

            isPaused = true;
            _gameManager.PauseGame(isPaused);
        }
        else if (isPaused == true)
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            FindObjectOfType<AudioManager>().UnPause("ParesSFX");
            FindObjectOfType<AudioManager>().UnPause("KaninSFX");


            isPaused = false;
            _gameManager.PauseGame(isPaused);
        }

    }
}
