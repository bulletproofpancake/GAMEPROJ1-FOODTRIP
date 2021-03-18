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
    [SerializeField] private GameObject background;
    public bool isPaused;
    
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
        if (isVN && completedCustomers > customers.Count)
        {
            //TODO: EARN MONEY AFTER FINISHING VN
            //SpawnManager.Instance.ClearLists();
            if (ShiftManager.Instance.shift != null)
                SceneSelector.Instance.LoadNextScene("Summary");
            else
            {
                SceneSelector.Instance.LoadNextScene("Main Menu");
            }
        }
        else
        {
            if(!isVN)
                moneyTextArcade.text = $"{MoneyManager.Instance.currentMoney}";
            else
                moneyTextVN.text = $"{MoneyManager.Instance.currentMoney}";
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

        if(!IsEncounterComplete())
        {
            SceneSelector.Instance.LoadNextScene($"Scenes/Game Scenes/{ShiftManager.Instance.cart.Type}/VN");
        }
        else
            SceneSelector.Instance.LoadNextScene("Summary");
    }

    public bool IsEncounterComplete()
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
            StartCoroutine(CountDownLevel());
        }
    }
}
