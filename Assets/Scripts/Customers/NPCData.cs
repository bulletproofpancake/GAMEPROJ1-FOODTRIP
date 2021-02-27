using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Data/New NPC")]
public class NPCData : CustomerData
{
    [SerializeField] private ShiftSchedule appearsIf;
    public ShiftSchedule AppearsIf => appearsIf;

}
