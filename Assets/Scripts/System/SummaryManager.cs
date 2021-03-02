using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customersServed;
    [SerializeField] private TextMeshProUGUI expectedProfit;
    [SerializeField] private TextMeshProUGUI actualProfit;
    [SerializeField] private TextMeshProUGUI totalMoney;

    private void Start()
    {
        actualProfit.text = $"Actual Profit: {DataManager.Instance.roundMoney}";
        totalMoney.text = $"Total Money: {DataManager.Instance.totalMoney}";
    }
}
