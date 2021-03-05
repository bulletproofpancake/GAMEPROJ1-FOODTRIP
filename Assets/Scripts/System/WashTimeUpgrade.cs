using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashTimeUpgrade : UpgradeSystem
{
    [SerializeField] private Bowl bowl;

    public void UpgradeBowlWashTime()
    {
        if(Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[Level].cost;

                bowl.ReduceWashTime(upgradeData.Upgrade[Level].Multiplier);
                
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
