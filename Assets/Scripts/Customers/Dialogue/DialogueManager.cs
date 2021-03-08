using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    public NPCData data;
    [SerializeField] private TextMeshProUGUI nameBox, dialogueBox;
    public int dataIndex, textIndex;
    [SerializeField] private GameObject customerObject;
    private Customer _customer;
    private SpriteRenderer _spriteRenderer;
    private Sprite _base;

    private void OnEnable()
    {
        nameBox.text = data.name;
    }

    private void OnDisable()
    {
        dataIndex++;
        textIndex = 0;
    }
    
    public void GetCustomerObject(GameObject customer)
    {
        customerObject = customer;
        _customer = customerObject.GetComponent<Customer>();
        _spriteRenderer = customerObject.GetComponent<SpriteRenderer>();
        _base = _spriteRenderer.sprite;
    }
    
    public void Advance()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        
        if (textIndex < data.Encounter[data.Count].dialogueDatas[dataIndex].Info.Length)
        {
            _spriteRenderer.sprite = data.Encounter[data.Count].dialogueDatas[dataIndex].Info[textIndex].sprite;
            dialogueBox.text = data.Encounter[data.Count].dialogueDatas[dataIndex].Info[textIndex].text;
            textIndex++;
        }
        else
        {
            gameObject.SetActive(false);
            _spriteRenderer.sprite = _base;
            _customer.orderBox.SetActive(true);
        }
    }
    
}
