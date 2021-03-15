using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;
    //private Button button;

    private void Start()
    {
        //button = gameObject.GetComponent<Button>();

        infoBox.SetActive(false);
    }


    public void ShowInfo()
    {
        if (Pause.isPaused == true)// Closes Info Box
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            FindObjectOfType<AudioManager>().UnPause("ParesSFX");
            FindObjectOfType<AudioManager>().UnPause("KaninSFX");

            Pause.isPaused = false;
            infoBox.SetActive(false);
            print("Info open (PAUSED) = " + Pause.isPaused);
        }
        else// Opens Info Box
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            FindObjectOfType<AudioManager>().Pause("ParesSFX");
            FindObjectOfType<AudioManager>().Pause("KaninSFX");

            Pause.isPaused = true;
            infoBox.SetActive(true);
            print("Info open (PAUSED) = " + Pause.isPaused);

        }
    }
}
