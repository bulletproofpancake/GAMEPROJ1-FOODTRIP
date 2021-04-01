using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashTimeUpgrade : UpgradeSystem
{
    [SerializeField] private GameObject bowlObject;
    [SerializeField] private GameObject cupObject;
    private Bowl bowl;
    private Bowl cup;
    private void Awake()
    {
        bowl = bowlObject.GetComponent<Bowl>();
        cup = cupObject.GetComponent<Bowl>();
    }

    public void UpgradeBowlWashTime()
    {
        if(upgradeData.Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[upgradeData.Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[upgradeData.Level].cost;

                bowl.ReduceWashTime(upgradeData.Upgrade[upgradeData.Level].Multiplier);
                cup.ReduceWashTime(upgradeData.Upgrade[upgradeData.Level].Multiplier);
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
        Tooltip.ShowTooltip_Static("Cleaning bowls is now faster than ever!");
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
    }
}
