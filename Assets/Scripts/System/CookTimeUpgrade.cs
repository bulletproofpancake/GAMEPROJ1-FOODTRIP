using System;
using UnityEngine;

public class CookTimeUpgrade : UpgradeSystem
{
    [SerializeField] private OrderData[] orderData;

    public void UpgradeCookTime()
    {
        if(Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[Level].cost;

                foreach (var data in orderData)
                {
                    data.ReduceCookTime(upgradeData.Upgrade[Level].Multiplier);
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
