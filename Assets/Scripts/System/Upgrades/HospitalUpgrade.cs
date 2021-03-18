using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalUpgrade : UpgradeSystem
{
    protected override void Update()
    {
        base.Update();
        if (upgradeData.Level == upgradeData.Upgrade.Length)
        {
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
}
