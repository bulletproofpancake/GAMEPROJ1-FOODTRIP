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
            isPaused = true;
            _gameManager.PauseGame(isPaused);
        }
        else if (isPaused == true)
        {
            isPaused = false;
            _gameManager.PauseGame(isPaused);
        }

    }
}
