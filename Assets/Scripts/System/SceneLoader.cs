using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneSelector.Instance.LoadNextScene(0);
    }

    public void LoadNextScene()
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
