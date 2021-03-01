using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;
    public GameObject customerObject;
    
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private TextMeshProUGUI textBox;

    private Customer _customer;
    private SpriteRenderer _spriteRenderer;
    private int _index;

    private void OnEnable()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        yield return new WaitForSeconds(.01f);
        customerObject = SpawnManager.Instance.vnCharacter;
        _customer = customerObject.GetComponent<Customer>();
        _spriteRenderer = customerObject.GetComponent<SpriteRenderer>();
        AdvanceDialogue();

    }
    
    public void AdvanceDialogue()
    {
        if(_index < dialogueData.Dialogue.Length)
        {
            nameBox.text = dialogueData.NPC.name;
            _spriteRenderer.sprite = dialogueData.Dialogue[_index].sprite;
            textBox.text = dialogueData.Dialogue[_index].text;
            _index++;
        }
        else
        {
            Debug.LogWarning("No more dialogue left");
            gameObject.SetActive(false);
            _customer.orderBox.SetActive(true);
        }
    }
    
}
