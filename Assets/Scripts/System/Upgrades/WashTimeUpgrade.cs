using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashTimeUpgrade : UpgradeSystem
{
    [SerializeField] private GameObject bowlObject;
    private Bowl bowl;
    private void Awake()
    {
        bowl = bowlObject.GetComponent<Bowl>();
    }

    public void UpgradeBowlWashTime()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        if (upgradeData.Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[upgradeData.Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[upgradeData.Level].cost;

                bowl.ReduceWashTime(upgradeData.Upgrade[upgradeData.Level].Multiplier);
                
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
}
