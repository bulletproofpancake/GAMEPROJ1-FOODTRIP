using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftLoader : SceneLoader
{
    [SerializeField] private ShiftData data;

    public override void LoadNextScene()
    {
        ShiftManager.Instance.Data = data;
        base.LoadNextScene();
    }
}
