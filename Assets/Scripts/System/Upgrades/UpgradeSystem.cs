using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSystem:MonoBehaviour
{
    [SerializeField] protected UpgradeData upgradeData;
    [SerializeField] protected TextMeshProUGUI upgradeText, currentText;
    
    private void Update()
    {
        if(upgradeData.Level < upgradeData.Upgrade.Length)
            upgradeText.text = $"Cost: {upgradeData.Upgrade[upgradeData.Level].cost}";
        else
            upgradeText.text = "Upgrade Complete";
        
        currentText.text = $"Level: {upgradeData.Level}";
    }
}