using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JK_Main_Menu : SceneLoader
{
    [SerializeField] private CartData data;
    [SerializeField] GameObject AreaSelect;
    [SerializeField] GameObject ShiftSelect;
    bool isActive;

    public void AreaSelectMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        if (AreaSelect != null)
        {
            isActive = AreaSelect.activeSelf;
            AreaSelect.SetActive(!isActive);
        }
    }

    public void ShiftSelectMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        if (ShiftSelect != null)
        {
            ShiftManager.Instance.cart = data;
            isActive = ShiftSelect.activeSelf;
            ShiftSelect.SetActive(!isActive);
        }
    }
}
