﻿using System.Collections;
using Customers;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    [SerializeField] private DialogueData dialogueData;

    [SerializeField] private GameObject orderIcon;
    [SerializeField] private TextMeshPro orderText;

    [SerializeField] private SpriteRenderer dialogueBox;
    [SerializeField] private Sprite DialogueBoxNormal;
    [SerializeField] private Sprite dialogueBoxPaid;

    [SerializeField] private Transform originalPos;
    [SerializeField] private Transform endPos;

    private Order _currentOrder;
    private SpriteRenderer _spriteRenderer;

    private float _numberOfOrders;
    private float _completedOrders;

    private float _paymentContainer;
    private bool readyToCollect;
    
    public int SeatTaken { get; set; }


    public GameObject particleEffect;
    

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.ChangeSprite();
        SetOrder();
        GiveOrder();
    }

    private void OnDisable()
    {
        particleEffect.SetActive(false);
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
        _paymentContainer = 0;
        dialogueBox.sprite = DialogueBoxNormal;
        orderIcon.GetComponent<SpriteRenderer>().enabled = true;
        readyToCollect = false;

        gameObject.transform.position = originalPos.position;
    }
    
    private void SetOrder()
    {
        _completedOrders = 0;
        _numberOfOrders = Random.Range(0, data.PossibleOrders.Length);
    }
    
    private void GiveOrder()
    {
        _currentOrder = data.SelectOrder();
        orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
        orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
    }

    private void TakeOrder(Order givenOrder)
    {
        int _index;

        if (_currentOrder.Data == givenOrder.Data)
        {
            StartCoroutine(ShowParticleEffect());
            _completedOrders++;

            _paymentContainer += givenOrder.Data.Cost;

            orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";

            if (_completedOrders >= _numberOfOrders + 1)
            {
                dialogueBox.sprite = dialogueBoxPaid;
                readyToCollect = true;

                _index = Random.Range(0, dialogueData.customerDialogue.Length);

                orderIcon.GetComponent<SpriteRenderer>().enabled = false;
                orderText.text = dialogueData.customerDialogue[_index].Dialogue;

                StartCoroutine(CustomerDespawn());
            }
        }
        else
        {
            StartCoroutine(WrongOrder());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Order>())
        {
            TakeOrder(other.GetComponent<Order>());
        }
    }

    private IEnumerator WrongOrder()
    {
        orderIcon.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        orderIcon.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator CustomerDespawn()
    {
        yield return new WaitForSeconds(data.DespawnTime);
        LeanTween.moveX(gameObject, endPos.position.x, 2f);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (readyToCollect == true)
        {
            FindObjectOfType<AudioManager>().Play("CustomerPay");
            MoneyManager.Instance.Collect(_paymentContainer);

            StartCoroutine(CustomerLeave());
        }
    }

    private IEnumerator CustomerLeave()
    {
        LeanTween.moveX(gameObject, endPos.position.x, 2f);
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }

    private IEnumerator ShowParticleEffect()
    {
        particleEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        particleEffect.SetActive(true);
    }
}
