using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCostUpgrade : UpgradeSystem
{
    [SerializeField] private OrderData[] orderData;

    public void UpgradeOrderCost()
    {
        if(Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[Level].cost;

                foreach (var data in orderData)
                {
                    data.IncreaseOrderCost(upgradeData.Upgrade[Level].Multiplier);
                }
                if (Level < upgradeData.Upgrade.Length)
                    Level++;
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
