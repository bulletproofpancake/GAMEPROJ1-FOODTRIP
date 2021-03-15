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

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        MoneyManager.Instance.EndRound();
        SceneSelector.Instance.LoadNextScene(0);
    }

    public void LoadArcade()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        FindObjectOfType<AudioManager>().Play("ArcadeBGM");

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        SceneSelector.Instance.LoadNextScene("Arcade");
    }

    public void LoadSummary()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        SceneSelector.Instance.LoadNextScene("Summary");
    }

    public void LoadUpgrades()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        SceneSelector.Instance.LoadNextScene("Upgrades");
    }

    public virtual void LoadNextScene()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        SceneSelector.Instance.LoadNextScene();
    }

    public void LoadPreviousScene()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneSelector.Instance.LoadPreviousScene();
    }

    public void Exit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        LeanTween.scale(gameObject, new Vector3(.3f, .3f, .3f), 1f).setEase(LeanTweenType.punch);

        Application.Quit();
    }
}
