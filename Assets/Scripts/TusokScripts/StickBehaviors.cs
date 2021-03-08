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

  //stack for the food
  private Stack<GameObject> foodStack = new Stack<GameObject>();

  [SerializeField]
  private AreaToStick[] foodStickPos = new AreaToStick[3];

  [SerializeField]
  private Transform pooledObjectReference;





  private void Start()
  {
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      Debug.Log("Did Press E");
      PopOutFood();
    }

    LaserPointer();
    OnDrawGizmos();


  }

  //! REDO LASERPOINTER FUNCTION
  // TODO make sure to add controls to raycasting

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
        Debug.Log("Did Press");
        hitFood.transform.rotation = Quaternion.Euler(Vector3.zero);
        StackInFood(hitFood);
        //return;
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
    // TODO make sure to optimize this line of code
    // ! still buggy. It bugs aorund at certain positions randomly. 
    // ! lowering the sensitivity could be the solution but UX feels like a heavy stick
    transform.RotateAround(referencePosition.position, zAxis, Input.GetAxis("Mouse X") * 480f * Time.deltaTime);

    // for mouse dragging
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    transform.Translate(mousePos);


  }


  //function that puts the food in the stick.
  // TODO make this so that it uses Stack 
  // ! not yet done. 
  private void StackInFood(GameObject food)
  {
    food.GetComponent<Rigidbody2D>().isKinematic = true;
    food.GetComponent<Rigidbody2D>().Sleep();
    food.GetComponent<Rigidbody2D>().freezeRotation = true;

    foodStack.Push(food);
    int index = 0;
    foreach (GameObject foods in foodStack)
    {

      food.transform.position = foodStickPos[index].stickInPos.position;
      food.transform.parent = foodStickPos[index].stickInPos.transform;

      index++;
    }
  }

  //function that removes the food from the object
  // TODO implement this so that 
  private void PopOutFood()
  {
    GameObject getTopObject;


    getTopObject = foodStack.Pop(); // this just references the top of the food object
    foodStack.Pop();
    // this checks if the Stack contains a gameobject
    if (getTopObject == null)
    {
      Debug.LogWarning("There is no food in this stick.");
    }


    //getTopObject.transform.localScale *= 2.0f;
    getTopObject.GetComponent<Rigidbody2D>().isKinematic = false;
    getTopObject.GetComponent<Rigidbody2D>().WakeUp();
    getTopObject.GetComponent<Rigidbody2D>().freezeRotation = false;


    getTopObject.transform.parent = pooledObjectReference.transform; // food goes back to the pooledObjects


    //test 
  }

}