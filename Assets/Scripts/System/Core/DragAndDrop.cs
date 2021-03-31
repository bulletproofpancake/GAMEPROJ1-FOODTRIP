using System;
using UnityEngine;

//following: https://youtu.be/55TBhlOt_U8
public class DragAndDrop : MonoBehaviour
{
    private bool _isDragging;
    private Camera _camera;
    private Vector2 _mousePos;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        _isDragging = true;
    }
    
    public void OnMouseUp()
    {
        _isDragging = false;
    }

    private void Update()
    {
        if (!_isDragging) return;
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(_mousePos);
        RestrictMovement();
    }

    private void OnDisable()
    {
        OnMouseUp();
    }
    
    private void RestrictMovement()
    {
        var upperRightCorner = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var lowerLeftCorner = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        var bounds = _spriteRenderer.bounds;
        var playerWidth = bounds.size.x / 2;
        var playerHeight = bounds.size.y / 2;

        var position = transform.position;
        var xVal = Mathf.Clamp(position.x, lowerLeftCorner.x + playerWidth, upperRightCorner.x - playerWidth);
        var yVal = Mathf.Clamp(position.y, lowerLeftCorner.y + playerHeight, upperRightCorner.y - playerHeight);

        position = new Vector3(xVal, yVal, 0);
        transform.position = position;
    }
    
}
