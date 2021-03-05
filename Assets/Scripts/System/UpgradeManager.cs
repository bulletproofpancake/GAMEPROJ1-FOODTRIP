using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private UpgradeData upgradeData;
    [SerializeField] private OrderData[] orderData;
    private int _level;
    
    private void Update()
    {
        moneyText.text = $"{MoneyManager.Instance.totalMoney}";
    }

    public void Upgrade()
    {
        if(_level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[_level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[_level].cost;

                foreach (var data in orderData)
                {
                    data.ReduceCookTime(upgradeData.Upgrade[_level].Multiplier);
                }

                if (_level < upgradeData.Upgrade.Length)
                    _level++;
            }
            else
            {
                Debug.LogWarning("Not enough money");
            }
        }
        else
        {
            Debug.LogWarning("No upgrades left");
        }
    }
    
}