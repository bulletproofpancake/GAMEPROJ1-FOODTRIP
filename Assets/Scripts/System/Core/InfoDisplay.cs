using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;

    void Start()
    {
        infoBox.SetActive(false);
    }

    public void ShowInfo()
    {
        if(Pause.isPaused==true)
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            //UnPauseAudio();

            Pause.isPaused = false;
            Time.timeScale = 1f;
            infoBox.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            //PauseAudio();

            Pause.isPaused = true;
            Time.timeScale = 0f;
            infoBox.SetActive(true);
        }
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

