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
  #region Revolving_stick

  [Tooltip("reference position for the stick to revolve around to")]
  [SerializeField]
  private Transform referencePosition;

  Vector3 zAxis = new Vector3(0, 0, 1);
  Vector2 mousePos;
  #endregion


  //variables used for Line Renderer.
  #region Raycast_pointer

  public LineRenderer lineRenderer;
  public Transform startPoint;
  public LayerMask layer;

  private const int MAX_RAYCASTDISTANCE = 100;

  #endregion


  // ? STACK DATA STRUCT FOR GROUPING FOODS IN THE STICK
  // ! MIGHT NOT USE THIS ON THE LONG RUN. WILL STILL KEEP AN EYE OUT IN CASE I FIND A WAY FOR THIS TO BE USEFUL
  //stack for the food
  private Stack<GameObject> foodStack = new Stack<GameObject>();

  [SerializeField]
  private AreaToStick[] foodStickPos = new AreaToStick[3];

  // * USE LIST INSTEAD FOR GROUPING FOODS IN THE STICK
  private List<GameObject> foodsInTheStick = new List<GameObject>();

  [SerializeField]
  private Transform pooledObjectReference;


  private void Start()
  {


  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      RemoveFood();
      Debug.Log("Did Press E");
    }

    LaserPointer();
    OnDrawGizmos();


  }

  // ! MIGHT REDO THE CODE AGAIN
  private void LaserPointer()
  {
    //Setting up 2d ray 
    Ray2D ray = new Ray2D(startPoint.position, -transform.up);
    //separated raycast2d outside the if statement just in case this is still needed for other raycast
    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    DrawRay2D(ray.origin, hit.point);

    var hitFood = hit.collider.gameObject;



    if (hit.collider != null)
    {

      // ! there are times where it does not pick up the food
      if (hitFood != null && hitFood.GetComponent<Order>() && Input.GetMouseButtonDown(1))
      {
        int counter = 0;
        // for loop checks if there is still space within the stick
        for (int i = 0; i < foodStickPos.Length; i++)
        {
          if (!foodStickPos[i].doesContain)
            break; // detects that there is still space in the stick
          else
            counter++;

        }
        if (counter > foodStickPos.Length - 1)
        {// checks if the coutner has gone atleast 3 
          Debug.LogWarning("stick is full!");
          return;

        }

        hitFood.transform.rotation = Quaternion.Euler(Vector3.zero);
        StackInFood(hitFood);
      }
    }

  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawRay(startPoint.position, -transform.up * MAX_RAYCASTDISTANCE);
  }

  public void DrawRay2D(Vector2 startPos, Vector2 endPos)
  {
    lineRenderer.SetPosition(0, startPos);
    lineRenderer.SetPosition(1, endPos);

  }

  //function that enables to drag the stick around.
  private void OnMouseDrag()
  {
    // this is the line of code that rotates the stick around. 
    // TODO redo the rotation. Somehow, the reference comes from above
    transform.RotateAround(referencePosition.position, zAxis, Input.GetAxisRaw("Mouse X") * 500f * Time.deltaTime);

    // for mouse dragging
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    transform.Translate(mousePos);


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
  private void RemoveFood()
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

}