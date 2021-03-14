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
        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);
        MoneyManager.Instance.EndRound();
        SceneSelector.Instance.LoadNextScene(0);
    }

    public void LoadSummary()
    {
        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);
        SceneSelector.Instance.LoadNextScene("Summary");
    }

    public void LoadUpgrades()
    {
        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);
        SceneSelector.Instance.LoadNextScene("Upgrades");
    }

    public virtual void LoadNextScene()
    {
        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);
        SceneSelector.Instance.LoadNextScene();
    }

    public void LoadPreviousScene()
    {
        SceneSelector.Instance.LoadPreviousScene();
    }

    public void Exit()
    {
        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);
        Application.Quit();
    }
    
}
