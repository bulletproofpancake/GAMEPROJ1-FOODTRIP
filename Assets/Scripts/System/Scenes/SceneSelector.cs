using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : Singleton<SceneSelector>
{

    public Animator transition;
    [SerializeField] private float transitionTime;
    
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
        transition.Play("Crossfade_Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
    private IEnumerator LoadScene(string path)
    {
        transition.Play("Crossfade_Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(path);
    }

}
