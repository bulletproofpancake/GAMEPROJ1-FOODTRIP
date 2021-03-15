using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummaryManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI customersServed;
    [SerializeField] private TextMeshProUGUI expectedProfit;
    [SerializeField] private TextMeshProUGUI actualProfit;
    [SerializeField] private TextMeshProUGUI totalMoney;

    private void Start()
    {
        if(ShiftManager.Instance.Data != null)
            SetBackgroundImage();
        
        actualProfit.text = $"Actual Profit: {MoneyManager.Instance.roundMoney}";
        totalMoney.text = $"Total Money: {MoneyManager.Instance.totalMoney}";
    }

    private void SetBackgroundImage()
    {
        switch (ShiftManager.Instance.Data.Schedule)
        {
            case ShiftSchedule.Morning:
                image.sprite = ShiftManager.Instance.Data.LocSprites.Morning;
                break;
            case ShiftSchedule.Afternoon:
                image.sprite = ShiftManager.Instance.Data.LocSprites.Afternoon;
                break;
            case ShiftSchedule.Night:
                image.sprite = ShiftManager.Instance.Data.LocSprites.Night;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
