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
        PlayButtonSFX();
        Tween();

        MoneyManager.Instance.EndRound();
        SceneSelector.Instance.LoadNextScene(0);
    }

    public void LoadSummary()
    {
        PlayButtonSFX();
        Tween();

        SceneSelector.Instance.LoadNextScene("Summary");
    }

    public void LoadUpgrades()
    {
        PlayButtonSFX();
        Tween();

        SceneSelector.Instance.LoadNextScene("Upgrades");
    }

    public void LoadTutorial()
    {
        PlayButtonSFX();
        Tween();
        Destroy(ShiftManager.Instance);
        SceneSelector.Instance.LoadNextScene("Tutorial");
    }

    public virtual void LoadNextScene()
    {
        PlayButtonSFX();
        Tween();

        SceneSelector.Instance.LoadNextScene();
    }
    
    public void LoadNextScene(string path)
    {
        PlayButtonSFX();
        Tween();

        SceneSelector.Instance.LoadNextScene(path);
    }
    
    public void LoadPreviousScene()
    {
        PlayButtonSFX();

        SceneSelector.Instance.LoadPreviousScene();
    }

    public void Exit()
    {
        PlayButtonSFX();
        Tween();

        Application.Quit();
    }
    
    void PlayButtonSFX()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    void Tween()
    {
        LeanTween.scale(gameObject, new Vector3(.7f, .7f, .7f), 1f).setEase(LeanTweenType.punch);
    }
}
