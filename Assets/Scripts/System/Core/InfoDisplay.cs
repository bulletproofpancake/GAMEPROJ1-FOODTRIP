using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoBox;
    [SerializeField] private GameManager _gameManager;

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
