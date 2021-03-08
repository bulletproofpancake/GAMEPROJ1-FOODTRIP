using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
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
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject background;

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
        }
        else
        {
            vnCanvas.SetActive(false);
            arcadeCanvas.SetActive(true);
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
        if (isVN && completedCustomers > customers.Count)
        {
            //TODO: EARN MONEY AFTER FINISHING VN
            SceneSelector.Instance.LoadNextScene();
        }
        else
        {
            moneyText.text = $"{MoneyManager.Instance.currentMoney}";
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
        MoneyManager.Instance.Earn();
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
}
