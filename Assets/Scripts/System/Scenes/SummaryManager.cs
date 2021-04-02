using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummaryManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image newBest;
    [SerializeField] private TextMeshProUGUI previousProfit;
    [SerializeField] private TextMeshProUGUI expectedProfit;
    [SerializeField] private TextMeshProUGUI currentProfit;
    [SerializeField] private TextMeshProUGUI totalMoney;
    private Color baseColor;

    private void Awake()
    {
        if(GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
    }

    private void Start()
    {
        if(ShiftManager.Instance.shift != null)
            SetBackgroundImage();

        baseColor = currentProfit.color;

        if (MoneyManager.Instance.previousMoney >= MoneyManager.Instance.currentMoney)
            newBest.gameObject.SetActive(false);
        else
            newBest.gameObject.SetActive(true);
        
        previousProfit.text = $"Previous Profit: {MoneyManager.Instance.previousMoney:F}";
        expectedProfit.text = $"Expected Profit: {MoneyManager.Instance.expectedMoney:F}";
        currentProfit.text = $"Current Profit: {MoneyManager.Instance.roundMoney:F}";
    }

    private void Update()
    {
        totalMoney.text = $"{MoneyManager.Instance.totalMoney:F}";
    }

    private void SetBackgroundImage()
    {
        switch (ShiftManager.Instance.shift.Schedule)
        {
            case ShiftSchedule.Morning:
                image.sprite = ShiftManager.Instance.shift.LocSprites.Morning;
                break;
            case ShiftSchedule.Afternoon:
                image.sprite = ShiftManager.Instance.shift.LocSprites.Afternoon;
                break;
            case ShiftSchedule.Night:
                image.sprite = ShiftManager.Instance.shift.LocSprites.Night;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
