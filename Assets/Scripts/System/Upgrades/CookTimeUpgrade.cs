using System;
using UnityEngine;

public class CookTimeUpgrade : UpgradeSystem
{
    [SerializeField] private OrderData[] orderData;

    public void UpgradeCookTime()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        if (upgradeData.Level < upgradeData.Upgrade.Length){
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
}
