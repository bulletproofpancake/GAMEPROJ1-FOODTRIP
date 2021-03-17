using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : Singleton<SceneSelector>
{

    public Animator transition;
    public Animator _sceneTransition;
    public float transitionTime;
    
    public void LoadPreviousScene()
    {
        if(SceneManager.GetActiveScene().buildIndex - 1 >= 0)
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
        }
        else
        {
            print("No Previous Scene Available");
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadNextScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    public void LoadNextScene(string path)
    {
        StartCoroutine(LoadScene(path));
    }
    
    private IEnumerator LoadScene(int index)
    {
        //Show designated transitions for the scene that will be loaded
        //if (index == 1)
        //    AreaSelectFromRight();
        //if (index == 2)
        //    ParesShiftFromRight();
        transition.Play("Crossfade_Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }

    private IEnumerator LoadScene(string path)
    {
        //Show designated transitions for the scene that will be loaded
        //if (path == "Area Select")
        //    AreaSelectFromRight();
        //if (path == "Shift Select")
        //    ParesShiftFromRight();
        transition.Play("Crossfade_Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(path);
    }

    #region Transitions
    void LoadMenuFromLeft()
    {
        _sceneTransition.Play("MenuFromLeft");
    }

    void LoadMenuFromRight()
    {
        _sceneTransition.Play("MenuFromRight");
    }

    void AreaSelectFromLeft()
    {
        _sceneTransition.Play("AreaSelectFromLeft");
    }

    void AreaSelectFromRight()
    {
        _sceneTransition.Play("AreaSelectFromRight");
    }

    void ParesMorning()
    {
        _sceneTransition.Play("ParesMorningFromRight");
    }

    void ParesAfternoon()
    {
        _sceneTransition.Play("ParesAfternoonFromRight");
    }

    void ParesNight()
    {
        _sceneTransition.Play("ParesNightFromRight");
    }

    void ParesShiftFromLeft()
    {
        _sceneTransition.Play("ParesShiftFromLeft");
    }

    void ParesShiftFromRight()
    {
        _sceneTransition.Play("ParesShiftFromRight");
    }

    void SummaryFromLeft()
    {
        _sceneTransition.Play("SummaryFromLeft");
    }

    void SummaryFromRight()
    {
        _sceneTransition.Play("SummaryFromRight");
    }
    #endregion
}
