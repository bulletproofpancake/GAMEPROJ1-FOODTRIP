using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/New Upgrade")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] private Upgrade[] upgrade;
    public Upgrade[] Upgrade => upgrade;
}

[System.Serializable]
public class Upgrade
{
    [SerializeField] private float multiplier;
    public float Multiplier => multiplier/100;

    public float cost;
}