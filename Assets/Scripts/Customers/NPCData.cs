using Customers.Dialogue;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Data/New NPC")]
public class NPCData : CustomerData
{
    [SerializeField] private ShiftSchedule appearsIf;
    public ShiftSchedule AppearsIf => appearsIf;
    [SerializeField] private NpcEncounter[] encounter;
    public NpcEncounter[] Encounter => encounter;
    [SerializeField] private int count;
    public int Count => count;
    
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    //TODO: WHAT HAPPENS WHEN COUNT EXCEEDS AVAILABLE ENCOUNTERS
    public void IncrementEncounter()
    {
        count++;
    }
}

[System.Serializable]
public class NpcEncounter
{
    public DialogueData[] dialogueDatas;
    public int DialogueCount => dialogueDatas.Length;
}