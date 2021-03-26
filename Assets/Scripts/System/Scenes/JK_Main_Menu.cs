using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JK_Main_Menu : SceneLoader
{
    [SerializeField] private CartData data;
    [SerializeField] GameObject AreaSelect;
    [SerializeField] GameObject ShiftSelect;
    bool isActive;



    private void update()
    {
        ShiftManager.Instance.cart = data;
    }

    public void AreaSelectMenu()
    {
        if (AreaSelect != null)
        {
            isActive = AreaSelect.activeSelf;
            AreaSelect.SetActive(!isActive);
        }
    }

    public void ShiftSelectMenu()
    {

        if (ShiftSelect != null)
        {
            ShiftManager.Instance.cart = data;
            isActive = ShiftSelect.activeSelf;
            ShiftSelect.SetActive(!isActive);
        }
    }
}
