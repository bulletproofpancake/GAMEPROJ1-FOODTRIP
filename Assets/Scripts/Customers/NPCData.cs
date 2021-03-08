using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Data/New NPC")]
public class NPCData : CustomerData
{
    [SerializeField] private NpcEncounter[] encounter;
    public NpcEncounter[] Encounter => encounter;
    [SerializeField] private int count;
    public int Count => count;
    
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void IncrementEncounter()
    {
        count++;
    }
}

[System.Serializable]
public class NpcEncounter
{
    public int encounter;
    public DialogueData[] dialogueDatas;
}