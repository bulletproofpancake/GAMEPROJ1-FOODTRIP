using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaToStick
{
     public Transform stickInPos;
     public bool doesContain;
}
public class StickBehaviors : MonoBehaviour
{

     //Variables used for rotating stick
     #region r_Stick_Drag_Variables

     Vector2 mousePos;


     [Space]

     //variables used for Line Renderer.
     public LineRenderer lineRenderer;

     //variable for 
     public Transform startPoint;

     private const int MAX_RAYCASTDISTANCE = 100;
     private bool didLeftClick;

     private bool disableMouseControl;

     #endregion

     #region r_Foods_In_Stick_Variables



     [Space]
     [Header("Food transfer variables")]
     [SerializeField]
     private AreaToStick[] foodStickPos = new AreaToStick[1];

     // * USE LIST INSTEAD FOR GROUPING FOODS IN THE STICK
     private List<GameObject> foodsInTheStick = new List<GameObject>();

     [SerializeField]
     private GameObject pooledObjectReference;

     private GameObject referencePoolObject;

     [SerializeField]
     private string pooledObjectName;

     [HideInInspector]
     public int counter;

     #endregion

     private void Start()
     {
          didLeftClick = false;
          disableMouseControl = false;
          counter = 0;
          //  referencePoolObject = GameObject.Find();

     }
     private void OnEnable()
     {
          pooledObjectReference = GameObject.Find(pooledObjectName);
     }
     private void Update()
     {

          Controls();
          LaserPointer();
     }

     private void Controls()
     {
          // * left click
          if (Input.GetMouseButtonDown(0) && !disableMouseControl)
          {
               didLeftClick = true;
          }

          // * right click
          if (Input.GetMouseButtonDown(1))
          {
               RemoveFood();
               Debug.Log("Dropped Item");
          }
          if (didLeftClick && !disableMouseControl)
               MoveStick();

          RotateStick();

     }

     private void LaserPointer()
     {
          //Setting up 2d ray 
          Ray2D ray = new Ray2D(startPoint.position, -transform.up);
          //separated raycast2d outside the if statement just in case this is still needed for other raycast
          RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
          var hitFood = hit.collider.gameObject;


          if (hitFood.GetComponent<Order>() || hit.collider.CompareTag("Kawali"))
          {
               lineRenderer.enabled = true;
               DrawRay2D(ray.origin, hit.point);
               if (hitFood != null && hitFood.GetComponent<Order>() && Input.GetMouseButtonDown(0))
               {
                    counter = 0;
                    // for loop checks if there is still space within the stick
                    for (int i = 0; i < foodStickPos.Length; i++)
                    {
                         if (!foodStickPos[i].doesContain)
                              break; // detects that there is still space in the stick
                         else
                              counter++;

                    }
                    Debug.Log("The amount of food in stick is: " + counter);
                    // ? This statement detects if the amount of food in stick is full or not
                    if (counter < foodStickPos.Length)
                    {

                         hitFood.transform.rotation = Quaternion.Euler(Vector3.zero);
                         StackInFood(hitFood);
                    }

               }
          }
          else
          {
               lineRenderer.enabled = false;
          }

     }


     public void DrawRay2D(Vector2 startPos, Vector2 endPos)
     {

          lineRenderer.SetPosition(0, startPos);
          lineRenderer.SetPosition(1, endPos);

     }

     private void MoveStick()
     {
          mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          transform.position = new Vector2(mousePos.x, mousePos.y);
     }

     private void RotateStick()
     {

          if (Input.GetAxis("Mouse ScrollWheel") > 0 && !disableMouseControl)
          {
               transform.Rotate(Vector3.forward * 5f, Space.Self);
          }
          if (Input.GetAxis("Mouse ScrollWheel") < 0 && !disableMouseControl)
          {
               transform.Rotate(Vector3.back * 5f, Space.Self);
          }
     }


     //function that puts the food in the stick.
     private void StackInFood(GameObject food)
     {
          food.GetComponent<Rigidbody2D>().isKinematic = true;
          food.GetComponent<Rigidbody2D>().Sleep();
          foodsInTheStick.Insert(0, food);

          SetFoodInStickPosition();

     }

     //function that removes the food from the stick
     public void RemoveFood()
     {
          GameObject foodOnTop = foodsInTheStick[0];
          int LastIndex = foodsInTheStick.Count - 1;

          foodOnTop.GetComponent<Rigidbody2D>().isKinematic = false;
          foodOnTop.GetComponent<Rigidbody2D>().WakeUp();

          foodOnTop.transform.parent = pooledObjectReference.transform;

          foodStickPos[LastIndex].doesContain = false;
          foodsInTheStick.Remove(foodOnTop);
          SetFoodInStickPosition();

     }

     private void SetFoodInStickPosition()

     {
          for (int index = 0; index <= foodsInTheStick.Count; index++)
          {
               foodsInTheStick[index].transform.position = foodStickPos[index].stickInPos.position;
               foodStickPos[index].doesContain = true;
               foodsInTheStick[index].transform.parent = foodStickPos[index].stickInPos.transform;
          }

     }


     private void OnTriggerStay2D(Collider2D other)
     {
          if (other.CompareTag("DirtyCup"))
          {

               if (Input.GetKeyDown("mouse 2") && counter >= 2)
               {
                    Debug.Log("middle click detected!");
                    disableMouseControl = true;
                    didLeftClick = false;

                    this.gameObject.SetActive(false);
                    // these are the lines of code if we don't want to make it disappear
                    // transform.parent = other.gameObject.transform;
                    // transform.position = other.gameObject.transform.position;
                    // transform.rotation = Quaternion.Euler(0, 0, 45f);

               }
               lineRenderer.enabled = false;
          }
     }

}