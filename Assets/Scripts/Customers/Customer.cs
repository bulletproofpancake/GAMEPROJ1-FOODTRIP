using System;
using System.Collections;
using Customers.Dialogue;
using Customers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
     private NPCData _npcData;
     [SerializeField] private CustomerData data;
     public CustomerData Data => data;
     [SerializeField] private GameObject orderIcon;
     [SerializeField] private TextMeshPro orderText;
     [SerializeField] private TextMeshPro promptBox;
     public GameObject orderBox;

     [SerializeField] private Customers.DialogueData dialogueData;
     [SerializeField] private SpriteRenderer dialogueBox;
     [SerializeField] private Sprite DialogueBoxNormal;
     [SerializeField] private Sprite dialogueBoxPaid;
     [SerializeField] private Image fillBackground;
     [SerializeField] private Image fillImage;

     private Order _currentOrder;
     private SpriteRenderer _spriteRenderer;
     private BoxCollider2D _boxCollider;

     private float _numberOfOrders;
     private float _completedOrders;

     private float _paymentContainer;
     private bool readyToCollect;
     private bool correctOrder;
     private bool wrongOrder;

     public int SeatTaken { get; set; }

     public GameObject particleEffect;

     private void Start()
     {
          if (GameManager.Instance.isVN)
          {
               orderBox.SetActive(false);
               GameManager.Instance.customers.Add(this);
               DialogueManager.Instance.GetCustomerObject(gameObject);
               DialogueManager.Instance.Advance();
          }
     }

     private void Update()
     {
          if (fillImage != null)
          {
               CustomerPatienceIndicator();
          }
     }

     private void OnEnable()
     {
          _spriteRenderer = GetComponent<SpriteRenderer>();
          promptBox.text = string.Empty;
          orderIcon.GetComponent<SpriteRenderer>().color = Color.white;
          _boxCollider = GetComponent<BoxCollider2D>();
          _boxCollider.enabled = true;
          _paymentContainer = 0;
          if (!GameManager.Instance.isVN)
          {
               //does not randomly change sprite if character is an NPC
               _spriteRenderer.sprite = data.ChangeSprite();
          }
          particleEffect.SetActive(false);

          if (fillBackground != null)
          {
               fillBackground.enabled = true;
          }

          if (fillImage != null)
          {
               fillImage.enabled = true;
               fillImage.fillAmount = data.DespawnTime;
          }
          StartCoroutine(FadeIn());

          if (!GameManager.Instance.isVN)
          {
               SetOrder();
               //Dictates the first order depending on the cart
               switch (ShiftManager.Instance.cart.Type)
               {
                    case CartType.Paresan:
                         OrderPares();
                         break;
                    case CartType.Tusoktusok:
                         GiveOrder();
                         break;
                    default:
                         throw new ArgumentOutOfRangeException();
               }
          }
     }

     private void OnDisable()
     {
          SpawnManager.spawner.customerSeat[SeatTaken].isTaken = false;
          dialogueBox.sprite = DialogueBoxNormal;
          orderIcon.GetComponent<SpriteRenderer>().enabled = true;
          readyToCollect = false;
          orderBox.SetActive(true);
     }

     public void SetOrder()
     {
          _completedOrders = 0;
          _numberOfOrders = Random.Range(0, data.PossibleOrders.Length);
     }

     public void GiveOrder()
     {
          _currentOrder = data.SelectOrder();
          orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
          orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
     }

     public void GiveOrder(Order order)
     {
          _currentOrder = order;
          orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
          orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
     }

     public void OrderPares()
     {
          _currentOrder = data.PossibleOrders[0];
          orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
          orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
     }

     private void TakeOrder(Order givenOrder)
     {
          if (!readyToCollect)
               givenOrder.gameObject.SetActive(false);
          else return;

          if (_currentOrder.Data == givenOrder.Data)
          {
               StartCoroutine(ShowParticleEffect());
               _completedOrders++;

               switch (data.Type)
               {
                    case CustomerType.None:
                         _paymentContainer += givenOrder.Data.Cost;
                         break;
                    case CustomerType.Student:
                         var incompletePayChance = Random.Range(1, 100);
                         //20% chance of customer not paying full
                         if (incompletePayChance <= 20)
                         {
                               print($"{incompletePayChance}");
                              _paymentContainer += ReducedPayment(givenOrder);
                         }
                         else
                              _paymentContainer += givenOrder.Data.Cost;
                         break;
                    default:
                         throw new ArgumentOutOfRangeException();
               }

               orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
               correctOrder = true;

               if (_completedOrders >= _numberOfOrders + 1)
               {
                    if (ShiftManager.Instance.cart != null)
                    {
                         if (ShiftManager.Instance.cart.Type == CartType.Tusoktusok &&
                             DirtyCupsScript.Instance.currentDirtyCupsInScene < DirtyCupsScript.Instance.maxAmountInScene)
                         {
                              //*insertdirty cup spawn here.
                              DirtyCupsScript.Instance.currentDirtyCupsInScene += 1;
                              Debug.Log("amount of DirtyCups in scene is " + DirtyCupsScript.Instance.currentDirtyCupsInScene);
                              DirtyCupsScript.Instance.SpawnHere();
                         }
                    }
                    if (FindObjectOfType<AudioManager>() != null)
                    {
                         FindObjectOfType<AudioManager>().Play("OrdersComplete");
                    }
                    dialogueBox.sprite = dialogueBoxPaid;
                    if (fillImage != null)
                    {
                         fillImage.fillAmount = data.DespawnTime;
                    }

                    if (GameManager.Instance.isVN)
                    {
                         _npcData = (NPCData)data;
                         if (_npcData.Encounter[_npcData.Count].DialogueCount > DialogueManager.Instance.dataIndex)
                         {
                              orderBox.SetActive(false);
                              dialogueBox.sprite = DialogueBoxNormal;
                              SetOrder();
                              GiveOrder();
                              DialogueManager.Instance.Advance();
                         }
                         else
                         {
                              readyToCollect = true;
                              OrderPrompt();
                         }

                    }
                    else
                    {
                         readyToCollect = true;
                         OrderPrompt();
                    }
               }
               else
               {
                    GiveOrder();
               }
          }
          else
          {
               wrongOrder = true;
               StartCoroutine(WrongOrder());
          }
     }

     private float ReducedPayment(Order givenOrder)
     {
          //amount customer will not pay is up to 20%
          // var rate = Random.Range(0, 20);
          //
          // var percent = rate / 100;
          //
          // var reduction = givenOrder.Data.Cost * percent;
          //
          // var payment = givenOrder.Data.Cost - reduction;
          //
          // print($"{rate},{reduction},{payment}");
          //
          // return payment;

          float rate = Random.Range(0, 20);
          var percent = rate / 100;
          var reduction = givenOrder.Data.Cost * percent;
          var payment = givenOrder.Data.Cost - reduction;
          
          print($"{rate},{reduction},{payment}");
          return payment;


     }
     private void OrderPrompt()
     {
          int _index = Random.Range(0, dialogueData.customerDialogue.Length);
          promptBox.text = dialogueData.customerDialogue[_index].Dialogue;
          orderIcon.GetComponent<SpriteRenderer>().enabled = false;
          orderText.text = $"Php. {_paymentContainer:F}";
     }

     private void OnTriggerEnter2D(Collider2D other)
     {

          if (ShiftManager.Instance.cart != null)
          {
               switch (ShiftManager.Instance.cart.Type)
               {
                    case CartType.Paresan:
                         if (other.GetComponent<Order>())
                              TakeOrder(other.GetComponent<Order>());
                         break;
                    case CartType.Tusoktusok:
                         StickBehaviors stick = other.GetComponent<StickBehaviors>();
                         if (other.GetComponent<Order>())
                         {
                              stick.RemoveFood();
                              TakeOrder(other.GetComponent<Order>());
                         }
                         break;
                    default:
                         throw new ArgumentOutOfRangeException();
               }
          }
          else if (other.GetComponent<Order>())
               TakeOrder(other.GetComponent<Order>());
     }

     private IEnumerator WrongOrder()
     {
          orderIcon.GetComponent<SpriteRenderer>().color = Color.red;
          yield return new WaitForSeconds(1f);
          orderIcon.GetComponent<SpriteRenderer>().color = Color.white;
     }

     private void OnMouseDown()
     {
          if (readyToCollect == true)
          {
               MoneyManager.Instance.Collect(_paymentContainer);
               _paymentContainer = 0;

               if (GameManager.Instance.isVN)
               {
                    if (!GameManager.Instance.isTutorial)
                    {
                         _npcData.IncrementEncounter();
                         MoneyManager.Instance.Earn();
                    }
                    GameManager.Instance.customers.Remove(this);
                    GameManager.Instance.completedCustomers++;
               }

               if (fillBackground != null)
               {
                    fillBackground.enabled = false;
               }

               if (fillImage != null)
               {
                    fillImage.enabled = false;
               }

               orderBox.SetActive(false);
               StartCoroutine(CustomerLeave());
          }
     }

     private IEnumerator CustomerLeave()
     {
          _boxCollider.enabled = false;
          // Fade Out
          for (float f = 1f; f >= -0.05f; f -= 0.05f)
          {
               Color c = _spriteRenderer.material.color;
               c.a = f;
               _spriteRenderer.material.color = c;

               yield return new WaitForSeconds(0.05f);
          }
          gameObject.SetActive(false);
     }

     private IEnumerator FadeIn()
     {
          for (float f = 0.05f; f <= 1; f += 0.05f)
          {
               Color c = _spriteRenderer.material.color;
               c.a = f;
               _spriteRenderer.material.color = c;

               yield return new WaitForSeconds(0.05f);
          }
     }

     private IEnumerator ShowParticleEffect()
     {
          particleEffect.SetActive(true);
          yield return new WaitForSeconds(1f);
          particleEffect.SetActive(false);
     }

     void CustomerPatienceIndicator()
     {
          fillImage.fillAmount -= 1.0f / data.DespawnTime * Time.deltaTime;

          if (fillImage.fillAmount == 0)
          {
               orderBox.SetActive(false);
               _boxCollider.enabled = false;
               Color c = _spriteRenderer.material.color;
               c.a -= 1.0f * Time.deltaTime;
               _spriteRenderer.material.color = c;
               Invoke("DisableObject", 1.0f);
          }

          //Add time
          if (correctOrder == true)
          {
               fillImage.fillAmount += data.DespawnTime * .07f;
               correctOrder = false;
          }

          //Reduce time
          if (wrongOrder == true)
          {
               fillImage.fillAmount -= data.DespawnTime * .07f;
               wrongOrder = false;
          }
     }

     void DisableObject()
     {
          gameObject.SetActive(false);
     }
}
