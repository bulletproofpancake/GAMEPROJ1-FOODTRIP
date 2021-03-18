using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;

    [SerializeField] private TextMeshProUGUI foodName, foodDescription;
    [SerializeField] private Image foodImage;
    [SerializeField] private InfoData pares, fishball, kwekkwek, kikiam;

    void Start()
    {
        infoBox.SetActive(false);
    }

    public void ShowInfo()
    {
        switch (ShiftManager.Instance.cart.Type)
        {
            case CartType.Paresan:
                ShowParesInfo();
                break;
            case CartType.Tusoktusok:
                ShowTusokTusokInfo();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

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

    private void ShowParesInfo()
    {
        foodName.text = pares.FoodName;
        foodDescription.text = pares.FoodDescription;
        foodImage.sprite = pares.FoodImage;
    }

    private void ShowTusokTusokInfo()
    {
        foodName.text = "Tusoktusok";
    }
}

