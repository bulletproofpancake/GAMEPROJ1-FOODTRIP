using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/New Upgrade")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] private int level;
    public int Level
    {
        get => level;
        set => level = value;
    }
    [SerializeField] private Upgrade[] upgrade;
    public Upgrade[] Upgrade => upgrade;
    
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
    
}

[System.Serializable]
public class Upgrade
{
    [SerializeField] private float multiplier;
    public float Multiplier => multiplier/100;

    public float cost;
}