using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;
    [SerializeField] private GameManager _gameManager;

    void Start()
    {
        infoBox.SetActive(false);
    }

    public void ShowInfo()
    {
        if(Pause.isPaused==true)
        {
            Pause.isPaused = false;
            _gameManager.PauseGame(false);
            infoBox.SetActive(false);
        }
        else
        {
            Pause.isPaused = true;
            _gameManager.PauseGame(true);
            infoBox.SetActive(true);
        }
    }
}
