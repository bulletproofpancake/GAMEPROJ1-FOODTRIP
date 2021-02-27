using System;
using UnityEngine;

//following: https://youtu.be/55TBhlOt_U8
public class DragAndDrop : MonoBehaviour
{
    private bool _isDragging;
    private Camera _camera;
    private Vector2 _mousePos;

    private void Awake()
    {
        _camera = Camera.main;
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
    }

    private void OnDisable()
    {
        OnMouseUp();
    }
}
