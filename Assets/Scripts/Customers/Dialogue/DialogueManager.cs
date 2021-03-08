using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private NPCData data;
    [SerializeField] private TextMeshProUGUI nameBox, dialogueBox;
}
