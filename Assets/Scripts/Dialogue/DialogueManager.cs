using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private TextMeshProUGUI textBox;

    private int _index;

    private void Awake()
    {
    }

    private void Start()
    {
        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        if(_index < dialogueData.Dialogue.Length)
        {
            nameBox.text = dialogueData.Dialogue[_index].npc.name;
            textBox.text = dialogueData.Dialogue[_index].text;
            _index++;
        }
        else
        {
            Debug.LogWarning("No more dialogue left");
            gameObject.SetActive(false);
            //GameManager.Instance.SpawnCart();
        }
    }
    
}
