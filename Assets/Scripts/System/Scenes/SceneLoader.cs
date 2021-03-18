using System;
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
        MoneyManager.Instance.EndRound();
        SceneSelector.Instance.LoadNextScene(0);
    }

    public void LoadSummary()
    {
        SceneSelector.Instance.LoadNextScene("Summary");
    }

    public void LoadUpgrades()
    {
        SceneSelector.Instance.LoadNextScene("Upgrades");
    }

    public void LoadTutorial()
    {
        SceneSelector.Instance.LoadNextScene("Tutorial");
    }

    public virtual void LoadNextScene()
    {
        SceneSelector.Instance.LoadNextScene();
    }
    
    public void LoadNextScene(string path)
    {
        SceneSelector.Instance.LoadNextScene(path);
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
