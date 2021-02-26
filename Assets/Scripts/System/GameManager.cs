using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private bool isVN;
    [SerializeField] private GameObject arcadeCanvas;
    [SerializeField] private GameObject vnCanvas;
    [SerializeField] private GameObject cartUsed;
    [SerializeField] private int levelDuration;
    [SerializeField] private ShiftData shiftData;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject background;
    
    private void Awake()
    {
        background.GetComponent<Canvas>().worldCamera = Camera.main;
        SetBackground();
        if (isVN)
        {
            arcadeCanvas.SetActive(false);
            Instantiate(vnCanvas, transform.position, Quaternion.identity);
        }
        else
        {
            vnCanvas.SetActive(false);
            Instantiate(arcadeCanvas, transform.position, Quaternion.identity);
        }

        Instantiate(cartUsed, transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneSelector.Instance.transition.Play("Crossfade_End");
        if (!isVN)
        {
            StartCoroutine(CountDownLevel());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVN && Input.GetKeyDown(KeyCode.Space))
        {
            SceneSelector.Instance.LoadNextScene();
        }
    }

    private IEnumerator CountDownLevel()
    {
        while (levelDuration > 0)
        {
            timerText.text = $"{levelDuration}";
            yield return new WaitForSeconds(1f);
            levelDuration--;
        }
        SceneSelector.Instance.LoadNextScene();
    }

    private void SetBackground()
    {
        switch (shiftData.Schedule)
        {
            case ShiftSchedule.Morning:
                background.GetComponentInChildren<Image>().sprite = shiftData.LocSprites.Morning;
                break;
            case ShiftSchedule.Afternoon:
                background.GetComponentInChildren<Image>().sprite = shiftData.LocSprites.Afternoon;
                break;
            case ShiftSchedule.Night:
                background.GetComponentInChildren<Image>().sprite = shiftData.LocSprites.Night;
                break;
        }
    }
}
