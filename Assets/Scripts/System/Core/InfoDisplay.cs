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
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            FindObjectOfType<AudioManager>().UnPause("ParesSFX");
            FindObjectOfType<AudioManager>().UnPause("KaninSFX");

            Pause.isPaused = false;
            _gameManager.PauseGame(false);
            infoBox.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            FindObjectOfType<AudioManager>().Pause("ParesSFX");
            FindObjectOfType<AudioManager>().Pause("KaninSFX");

            Pause.isPaused = true;
            _gameManager.PauseGame(true);
            infoBox.SetActive(true);


        }
    }
}
