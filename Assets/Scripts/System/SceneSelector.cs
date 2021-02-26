using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : Singleton<SceneSelector>
{
    public void LoadPreviousScene()
    {
        if(SceneManager.GetActiveScene().buildIndex - 1 >= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            print("No Previous Scene Available");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
