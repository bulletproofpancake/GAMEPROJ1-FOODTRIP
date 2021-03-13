using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftLoader : SceneLoader
{
    [SerializeField] private ShiftData data;

    public override void LoadNextScene()
    {
        ShiftManager.Instance.shift = data;
        //TODO: CREATE A MORE GENERIC LOADER
        if (ShiftManager.Instance.cart.Type == CartType.Tusoktusok)
        {
            base.LoadNextScene("tusoktusokTestScene");
        }
        else
        {
            base.LoadNextScene();
        }
    }
}
