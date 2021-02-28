using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Round Settings")]
    [SerializeField] private bool isVN;
    [SerializeField] private GameObject arcadeCanvas;
    [SerializeField] private GameObject vnCanvas;
    [SerializeField] private GameObject cartUsed;
    [SerializeField] private int levelDuration;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject background;

    [Header("VN Settings")]
    [SerializeField] private GameObject dialogueBox;

    protected override void Awake()
    {
        base.Awake();
        if(ShiftManager.Instance.Data != null){
            background.GetComponent<Canvas>().worldCamera = Camera.main;
            SetBackground();
        }
        
        if (isVN)
        {
            arcadeCanvas.SetActive(false);
            vnCanvas.SetActive(true);
            dialogueBox.SetActive(true);
            SpawnCart();
        }
        else
        {
            vnCanvas.SetActive(false);
            arcadeCanvas.SetActive(true);
            SpawnCart();
        }
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
        switch (ShiftManager.Instance.Data.Schedule)
        {
            case ShiftSchedule.Morning:
                background.GetComponentInChildren<Image>().sprite = ShiftManager.Instance.Data.LocSprites.Morning;
                break;
            case ShiftSchedule.Afternoon:
                background.GetComponentInChildren<Image>().sprite = ShiftManager.Instance.Data.LocSprites.Afternoon;
                break;
            case ShiftSchedule.Night:
                background.GetComponentInChildren<Image>().sprite = ShiftManager.Instance.Data.LocSprites.Night;
                break;
        }
    }

    public void SpawnCart()
    {
        Instantiate(cartUsed, transform.position, Quaternion.identity);
    }
    
}
