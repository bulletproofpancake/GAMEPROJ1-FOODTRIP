using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private NPCData npcData;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private TextMeshProUGUI textBox;

    private Customer _customer;
    private SpriteRenderer _spriteRenderer;
    private Sprite _baseSprite;

    private int _index;

    private void Awake()
    {
        _customer = GetComponentInParent<Customer>();
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        _baseSprite = _spriteRenderer.sprite;
    }

    private void Start()
    {
        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        if(_index < dialogueData.Dialogue.Length)
        {
            nameBox.text = npcData.name;
            _spriteRenderer.sprite = dialogueData.Dialogue[_index].sprite;
            textBox.text = dialogueData.Dialogue[_index].text;
            _index++;
        }
        else
        {
            Debug.LogWarning("No more dialogue left");
        }
    }
    
}
