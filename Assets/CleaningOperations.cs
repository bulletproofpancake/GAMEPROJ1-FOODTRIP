using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleaningOperations : MonoBehaviour
{

    [SerializeField] private Button paresButton;
    [SerializeField] private Button tusokButton;

    //Chance for having cleaning operations
    [SerializeField] private int chance = 60;

    private void OnEnable()
    {
        // run function wherein it activates a randomaizer chance
        int randomizer = Random.Range(0, 100);

        if (randomizer <= chance)
        {
            DisableButton();
        }

    }

    private void DisableButton()
    {
        int randomCart = Random.Range(0, 2);

        if (randomCart == 0)
        {
            paresButton.interactable = false;
            tusokButton.interactable = true;
        }
        else if (randomCart == 1)
        {
            paresButton.interactable = true;
            tusokButton.interactable = false;
        } 
    }

}
