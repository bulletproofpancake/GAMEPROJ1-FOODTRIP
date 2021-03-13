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
        base.LoadNextScene($"Scenes/Game Scenes/{ShiftManager.Instance.cart.Type}/Arcade");
    }
}
