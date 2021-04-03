using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [Header("VN Settings")]
    [SerializeField] private NPCData[] NpcDatas;
    
    [Header("Round Settings")]
    public bool isVN;
    public bool isTutorial;
    [SerializeField] private GameObject arcadeCanvas;
    [SerializeField] private GameObject vnCanvas;
    [SerializeField] private GameObject cartUsed;
    [SerializeField] private int levelDuration;
    public List<Customer> customers;
    public int completedCustomers;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI moneyTextArcade;
    [SerializeField] private TextMeshProUGUI moneyTextVN;
    [SerializeField] private TextMeshProUGUI overlay;
    [SerializeField] private GameObject background;
    public bool isPaused;

    public bool npcAvailable, encounterComplete, countdownFinished;

    public bool garbageFull;

    protected override void Awake()
    {
        base.Awake();
        if(ShiftManager.Instance.shift != null){
            background.GetComponent<Canvas>().worldCamera = Camera.main;
            SetBackground();
        }
        
        if (isVN)
        {
            arcadeCanvas.SetActive(false);
            vnCanvas.SetActive(true);
            print("VN Start");
        }
        else
        {
            vnCanvas.SetActive(false);
            arcadeCanvas.SetActive(true);
        }

        if(cartUsed!=null){
            Instantiate(cartUsed, transform.position, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SceneSelector.Instance.transition.Play("Crossfade_End");
        npcAvailable = IsNpcAvailable();
        encounterComplete = IsEncounterComplete();
        overlay.text = string.Empty;
        if (!isVN)
        {
            StartCoroutine(CountDownStart());
        }
        else
        {
            SpawnManager.spawner.SpawnVN();
        }
        
    }

    private IEnumerator CountDownStart()
    {
        timerText.text = levelDuration.ToString();
        countdownFinished = false;
        int i = 3;
        //PauseGame(true);
        while (i > 0)
        {
            overlay.text = i.ToString();
            yield return new WaitForSeconds(1f);
            i--;
        }
        countdownFinished = true;
        //PauseGame(false);
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Bell");
        }
        StartCoroutine(SpawnManager.spawner.SpawnCustomers());
        StartCoroutine(CountDownLevel());
        overlay.text = string.Empty;
    }
    // Update is called once per frame
    private void Update()
    {
        if (isVN && completedCustomers > customers.Count)
        {
            //TODO: EARN MONEY AFTER FINISHING VN
            //SpawnManager.Instance.ClearLists();
            if (ShiftManager.Instance.shift != null)
                SceneSelector.Instance.LoadNextScene("Summary");
            else
            {
                SceneSelector.Instance.LoadNextScene(0);
            }
        }
        else
        {
            if(!isVN)
                moneyTextArcade.text = $"{MoneyManager.Instance.currentMoney:F}";
            else
                moneyTextVN.text = $"{MoneyManager.Instance.currentMoney:F}";
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
        
        //SpawnManager.Instance.ClearLists();
        
        MoneyManager.Instance.Earn();
        
        FindObjectOfType<AudioManager>().Stop("ArcadeBGM");

        StartCoroutine(GameOver());

    }

    private void LoadNextLevel()
    {
        if (npcAvailable)
        {
            if (!encounterComplete)
            {
                SceneSelector.Instance.LoadNextScene($"Scenes/Game Scenes/{ShiftManager.Instance.cart.Type}/VN");
            }
            else
            {
                SceneSelector.Instance.LoadNextScene("Summary");
            }
        }
        else
        {
            SceneSelector.Instance.LoadNextScene("Summary");
        }
    }

    private IEnumerator GameOver()
    {
        Time.timeScale = 0f;
        overlay.text = "Game Over";
        print("game");
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Bell");
        }
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        print("over");
        LoadNextLevel();
    }
    
    private bool IsNpcAvailable()
    {
        foreach (var data in NpcDatas)
        {
            if(ShiftManager.Instance != null){
                if (data.AppearsIf == ShiftManager.Instance.shift.Schedule)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsEncounterComplete()
    {
        var isComplete = false;
        
        foreach (var data in NpcDatas)
        {
            if (data.AppearsIf == ShiftManager.Instance.shift.Schedule)
            {
                if (data.Count >= data.Encounter.Length)
                {
                    isComplete= true;
                }
                else
                {
                    isComplete= false;
                }
            }
            else
            {
                Debug.LogWarning("No suitable NPC found");
            }
        }

        return isComplete;
    }

    private void SetBackground()
    {
        switch (ShiftManager.Instance.shift.Schedule)
        {
            case ShiftSchedule.Morning:
                background.GetComponentInChildren<Image>().sprite = ShiftManager.Instance.shift.LocSprites.Morning;
                break;
            case ShiftSchedule.Afternoon:
                background.GetComponentInChildren<Image>().sprite = ShiftManager.Instance.shift.LocSprites.Afternoon;
                break;
            case ShiftSchedule.Night:
                background.GetComponentInChildren<Image>().sprite = ShiftManager.Instance.shift.LocSprites.Night;
                break;
        }
    }

    public void PauseGame(bool paused)
    {
        if (!isPaused)
        {
            isPaused = paused;
            Time.timeScale = 0f;
            print("Paused = " + isPaused);
            StopAllCoroutines();
        }
        else
        {
            isPaused = paused;
            Time.timeScale = 1f;
            print("Paused = " + isPaused);
            if(ShiftManager.Instance.shift !=null){
                if (countdownFinished)
                {
                    StartCoroutine(CountDownLevel());
                }
                else
                {
                    StartCoroutine(CountDownStart());
                }
            }
            else
            {
                StartCoroutine(CountDownLevel());
            }
        }
    }
}
