using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private UpgradeData upgradeData;
    private int _level;
    
    private void Update()
    {
        moneyText.text = $"{MoneyManager.Instance.totalMoney}";
    }

    public void UpgradeValue()
    {
        if (MoneyManager.Instance.totalMoney > upgradeData.Upgrade[_level].cost)
        {
            MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[_level].cost;
        }
    }
}
