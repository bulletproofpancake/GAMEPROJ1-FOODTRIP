using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JK_Main_Menu : SceneLoader
{
    [SerializeField] private CartData data;
    [SerializeField] GameObject AreaSelect;
    [SerializeField] GameObject ShiftSelect;
    bool isActive;

    //Hard set to avoid game opening conflict bugs
    private void Awake()
    {
        AreaSelect.SetActive(false);
        ShiftSelect.SetActive(false);
    }

    //Simplest way of opening and closing a canvas
    public void AreaSelectMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Tween();
        if (AreaSelect != null)
        {
            isActive = AreaSelect.activeSelf;
            AreaSelect.SetActive(!isActive);
        }
    }

    public void ShiftSelectMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Tween();
        if (ShiftSelect != null)
        {
            ShiftManager.Instance.cart = data;
            isActive = ShiftSelect.activeSelf;
            ShiftSelect.SetActive(!isActive);
        }
    }

    void Tween()
    {
        LeanTween.scale(gameObject, new Vector3(.7f, .7f, .7f), 1f).setEase(LeanTweenType.punch);
    }

    void Slide()
    {

    }
}
