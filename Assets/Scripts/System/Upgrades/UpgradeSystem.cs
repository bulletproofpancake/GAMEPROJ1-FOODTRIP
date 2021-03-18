using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem:MonoBehaviour
{
    [SerializeField] protected UpgradeData upgradeData;
    [SerializeField] protected TextMeshProUGUI upgradeCost;
    [SerializeField] protected Image[] upgradeSlot;

    private void Start()
    {
        foreach (var image in upgradeSlot)
        {
            image.color = Color.grey;
        }
    }

    protected virtual void Update()
    {
        if(upgradeData.Level < upgradeData.Upgrade.Length)
            upgradeCost.text = $"{upgradeData.Upgrade[upgradeData.Level].cost}";
        else
            upgradeCost.text = "Upgrade Complete";
        upgradeSlot[upgradeData.Level-1].color = Color.green;
    }
}