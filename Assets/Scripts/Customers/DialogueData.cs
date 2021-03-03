using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Data/New Dialogue")]
public class DialogueData : ScriptableObject
{
    public DialogueInfo[] customerDialogue;
}

[System.Serializable]
public class DialogueInfo
{
    [SerializeField] private string dialogue;
    public string Dialogue => dialogue;
}