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
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        MoneyManager.Instance.EndRound();
        SceneSelector.Instance.LoadNextScene(0);
    }

    public void LoadSummary()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneSelector.Instance.LoadNextScene("Summary");
    }

    public void LoadUpgrades()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneSelector.Instance.LoadNextScene("Upgrades");
    }

    public virtual void LoadNextScene()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneSelector.Instance.LoadNextScene();
    }

    public void LoadPreviousScene()
    {
        SceneSelector.Instance.LoadPreviousScene();
    }

    public void Exit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        Application.Quit();
    }
    
}
