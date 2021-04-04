using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customers.Dialogue
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        private NPCData _data;
        [SerializeField] private TextMeshProUGUI nameBox, dialogueBox;
        public int dataIndex, textIndex;
        [SerializeField] private GameObject customerObject;
        private Customer _customer;
        private SpriteRenderer _spriteRenderer;
        private Sprite _base;
        [SerializeField] private Order _order;

        private void OnDisable()
        {
            dataIndex++;
            textIndex = 0;
        }
    
        public void GetCustomerObject(GameObject customer)
        {
            customerObject = customer;
            _customer = customerObject.GetComponent<Customer>();
            _data = (NPCData)_customer.Data;
            _spriteRenderer = customerObject.GetComponent<SpriteRenderer>();
            _base = _spriteRenderer.sprite;
        }
    
        public void Advance()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            if (textIndex < _data.Encounter[_data.Count].dialogueDatas[dataIndex].Info.Length)
            {
                nameBox.text = _data.Encounter[_data.Count].dialogueDatas[dataIndex].Info[textIndex].speakerName;
                _spriteRenderer.sprite = _data.Encounter[_data.Count].dialogueDatas[dataIndex].Info[textIndex].sprite;
                //dialogueBox.text = _data.Encounter[_data.Count].dialogueDatas[dataIndex].Info[textIndex].text;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(_data.Encounter[_data.Count].dialogueDatas[dataIndex].Info[textIndex].text));
                _order = _data.Encounter[_data.Count].dialogueDatas[dataIndex].Info[textIndex].order;
                textIndex++;
            }
            else
            {
                if(_order!=null)
                {
                    if (ShiftManager.Instance.cart != null)
                    {
                        switch (ShiftManager.Instance.cart.Type)
                        {
                            case CartType.Paresan:
                                _customer.SetOrder();
                                _customer.GiveOrder(_order);
                                break;
                            case CartType.Tusoktusok:
                                _customer.SetOrder(2);
                                _customer.GiveOrder(_order);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        _customer.SetOrder();
                        _customer.GiveOrder(_order);
                    }
                }
                else
                {
                    if(ShiftManager.Instance.cart != null){
                        switch (ShiftManager.Instance.cart.Type)
                        {
                            case CartType.Paresan:
                                _customer.SetOrder();
                                _customer.OrderPares();
                                break;
                            case CartType.Tusoktusok:
                                _customer.SetOrder(2);
                                _customer.GiveOrder();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        _customer.SetOrder();
                        _customer.OrderPares();
                    }
                }
                gameObject.SetActive(false);
                _spriteRenderer.sprite = _base;
                _customer.orderBox.SetActive(true);
            }

            print(_order);
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueBox.text = "";

            foreach(char letter in sentence.ToCharArray())
            {
                dialogueBox.text += letter;
                yield return null;
            }
        }
    }
}
