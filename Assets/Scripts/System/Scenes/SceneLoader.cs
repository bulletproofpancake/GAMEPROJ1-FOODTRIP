﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneSelector.Instance.transition.Play("Crossfade_End");
    }

    public void LoadMainMenu()
    {
        DataManager.Instance.EndRound();
        SceneSelector.Instance.LoadNextScene(0);
    }

    public virtual void LoadNextScene()
    {
        SceneSelector.Instance.LoadNextScene();
    }

    public void LoadPreviousScene()
    {
        SceneSelector.Instance.LoadPreviousScene();
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
