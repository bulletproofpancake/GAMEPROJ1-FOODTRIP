using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JK_Main_Menu : SceneLoader
{
    [SerializeField] private CartData data;
    [SerializeField] GameObject AreaSelect;
    [SerializeField] GameObject ShiftSelect;
    //bool isActive;

    //Hard set to avoid game opening conflict bugs
    //private void Start()
    //{
    //    AreaSelect.SetActive(false);
    //    ShiftSelect.SetActive(false);
    //}

    //Simplest way of opening and closing a canvas

    private void Awake()
    {
        AudioManager.instance.Play("Main Menu");
    }

    public void AreaSelectMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Tween();
        if (AreaSelect != null)
        {
            //isActive = AreaSelect.activeSelf;
            //AreaSelect.SetActive(!isActive);
            AreaSelect.SetActive(true);
            LeanTween.moveX(AreaSelect, 0, 1f);
        }
    }

    public void ShiftSelectMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Tween();
        if (ShiftSelect != null)
        {
            ShiftManager.Instance.cart = data;
            //isActive = ShiftSelect.activeSelf;
            //ShiftSelect.SetActive(!isActive);
            ShiftSelect.SetActive(true);
            LeanTween.moveX(ShiftSelect, 0, 1f);
        }
    }

    public void CloseAreaSelect()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Tween();
        if (AreaSelect != null)
        {
            LeanTween.moveX(AreaSelect, 18, 1f);
            Invoke("AreaSelectDisable", 1f);
        }
    }

    public void CloseShiftSelect()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Tween();
        if (ShiftSelect != null)
        {
            LeanTween.moveX(ShiftSelect, 18, 1f);
            Invoke("ShiftSelectDisable", 1f);
        }
    }

    void AreaSelectDisable() {
        AreaSelect.SetActive(false);
    }

    void ShiftSelectDisable() {
        ShiftSelect.SetActive(false);
    }

    void Tween()
    {
        LeanTween.scale(gameObject, new Vector3(.7f, .7f, .7f), 1f).setEase(LeanTweenType.punch);
    }

    void Slide()
    {
    }
}
