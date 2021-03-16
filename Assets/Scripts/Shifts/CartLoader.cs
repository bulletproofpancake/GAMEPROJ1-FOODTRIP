using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLoader : SceneLoader
{
    [SerializeField] private CartData data;

    public override void LoadNextScene()
    {
        ShiftManager.Instance.cart = data;
        base.LoadNextScene();
    }
}
