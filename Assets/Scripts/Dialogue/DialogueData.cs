using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue",menuName = "Data/New Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private DialogueInfo[] dialogue;
    public DialogueInfo[] Dialogue => dialogue;
}

[System.Serializable]
public class DialogueInfo
{
    public NPCData npc;
    public Sprite sprite;
    [TextArea] public string text;
}
