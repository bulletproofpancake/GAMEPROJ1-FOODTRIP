using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBehaviors : MonoBehaviour
{
    #region RevolvingStick

    [Tooltip("reference position for the stick to revolve around to")]
    [SerializeField]
    private Transform referencePosition;

    Vector3 zAxis = new Vector3(0, 0, 1);
    Vector2 mousePos;
    #endregion


    #region raycastPointer

    public LineRenderer lineRenderer;
    public Transform pointDirection;
    public LayerMask layer;
    Transform startPoint;


    #endregion

    private void Start() {

        startPoint = GetComponent<Transform>();

        //transform.rotation = Quaternion.identity;
    }
    private void Update() {
        LaserPointer();

    }

    private void LaserPointer() {
        if (Physics2D.Raycast(startPoint.position, transform.right)) {
            RaycastHit2D hit = Physics2D.Raycast(pointDirection.position, transform.right);
            DrawRay2D(pointDirection.position, hit.point);
        } else {
            DrawRay2D(pointDirection.position, pointDirection.transform.right * 100.0f);
        }
    }

    public void DrawRay2D(Vector2 startPos, Vector2 endPos) {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

    }




    private void OnMouseDrag() {

        transform.RotateAround(referencePosition.position, zAxis, Input.GetAxis("Mouse X") * 720f * -Time.deltaTime);
        
        // for mouse dragging
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
 

    }

}
