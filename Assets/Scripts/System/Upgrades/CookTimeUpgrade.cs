using System;
using UnityEngine;

public class CookTimeUpgrade : UpgradeSystem
{
    [SerializeField] private OrderData[] orderData;

    public void UpgradeCookTime()
    {
        if(upgradeData.Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[upgradeData.Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[upgradeData.Level].cost;

                foreach (var data in orderData)
                {
                    data.ReduceCookTime(upgradeData.Upgrade[upgradeData.Level].Multiplier);
                }
                if (upgradeData.Level < upgradeData.Upgrade.Length)
                    upgradeData.Level++;
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

    private void OnMouseEnter()
    {
        Tooltip.ShowTooltip_Static("Cook Time");
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
    }
}
