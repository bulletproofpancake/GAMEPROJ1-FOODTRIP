using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalUpgrade : UpgradeSystem
{
    private void Awake()
    {
        if (upgradeData.Level == upgradeData.Upgrade.Length)
        {
            gameObject.SetActive(false);
        }

    }

    protected override void Update()
    {
        base.Update();
        if (upgradeData.Level == upgradeData.Upgrade.Length)
        {
            Destroy(ShiftManager.Instance);
            SceneSelector.Instance.LoadNextScene("Ending");
        }
    }

    public void PayHospital()
    {
        if(upgradeData.Level < upgradeData.Upgrade.Length){
            if (MoneyManager.Instance.totalMoney >= upgradeData.Upgrade[upgradeData.Level].cost)
            {
                MoneyManager.Instance.totalMoney -= upgradeData.Upgrade[upgradeData.Level].cost;
                
                if (upgradeData.Level < upgradeData.Upgrade.Length)
                    upgradeData.Level++;
            }
            else
            {
                Debug.LogWarning("Not enough money");
            }
        }
    }
    private void OnMouseEnter()
    {
        Tooltip.ShowTooltip_Static("Hospital bills to be paid for Manong Enteng");
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
    }
}
