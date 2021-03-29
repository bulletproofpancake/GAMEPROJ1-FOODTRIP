using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;

    [SerializeField] private TextMeshProUGUI foodName, foodDescription;
    [SerializeField] private Image foodImage;
    [SerializeField] private InfoData pares;
    [SerializeField] private InfoData[] tusoktusokInfo;

    [SerializeField] private int index = 0;
    [SerializeField] private GameObject[] navButtons;

    void Start()
    {
        infoBox.SetActive(false);
    }

    public void ShowInfo()
    {
        switch (ShiftManager.Instance.cart.Type)
        {
            case CartType.Paresan:
                foreach (var button in navButtons)
                {
                    button.SetActive(false);
                }
                ShowParesInfo();
                break;
            case CartType.Tusoktusok:
                foreach (var button in navButtons)
                {
                    button.SetActive(true);
                }
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

    public void IncrementIndex()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        index++;
        ShowTusokTusokInfo();
    }

    public void DecrementIndex()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        index--;
        ShowTusokTusokInfo();
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
        if (index >= tusoktusokInfo.Length)
            index = 0;
        else if (index < 0)
        {
            index = tusoktusokInfo.Length-1;
        }

        foodName.text = tusoktusokInfo[index].FoodName;
        foodDescription.text = tusoktusokInfo[index].FoodDescription;
        foodImage.sprite = tusoktusokInfo[index].FoodImage;
    }
}

