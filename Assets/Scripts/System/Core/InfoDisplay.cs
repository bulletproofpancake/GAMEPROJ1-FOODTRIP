using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;

    private void Start()
    {
        infoBox.SetActive(false);
    }

    public void ShowInfo()
    {
        if (Pause.isPaused == true)// Closes Info Box
        {
            Pause.isPaused = false;
            LeanTween.scale(infoBox, new Vector3(0, 0, 0), 0.5f);
            infoBox.SetActive(false);
        }
        else// Opens Info Box
        {
            Pause.isPaused = true;
            infoBox.SetActive(true);
        }
    }
}
