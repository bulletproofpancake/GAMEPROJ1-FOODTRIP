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
            Pause.isPaused = false;
            infoBox.SetActive(false);
            print("Info open (PAUSED) = " + Pause.isPaused);
        }
        else// Opens Info Box
        {
            Pause.isPaused = true;
            infoBox.SetActive(true);
            print("Info open (PAUSED) = " + Pause.isPaused);
        }
    }
}
