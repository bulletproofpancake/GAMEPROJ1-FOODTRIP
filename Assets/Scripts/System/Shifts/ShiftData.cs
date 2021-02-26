using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Shift", menuName = "Data/New Shift")]
public class ShiftData : ScriptableObject
{
    [SerializeField] private ShiftSchedule schedule;
    public ShiftSchedule Schedule => schedule;
    [SerializeField] private LocationSprites locSprites;
    public LocationSprites LocSprites => locSprites;
}

[System.Serializable]
public class LocationSprites
{
    [SerializeField] private Sprite morning;
    [SerializeField] private Sprite afternoon;
    [SerializeField] private Sprite night;
    
    public Sprite Morning => morning;
    public Sprite Afternoon => afternoon;
    public Sprite Night => night;
}

public enum ShiftSchedule
{
    Morning,
    Afternoon,
    Night
}