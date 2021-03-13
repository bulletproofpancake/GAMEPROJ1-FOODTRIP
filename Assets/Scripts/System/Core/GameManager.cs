using System.Collections;
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
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject background;
    public bool isPaused;
    
    private void Awake()
    {
        if(ShiftManager.Instance.Data != null){
            background.GetComponent<Canvas>().worldCamera = Camera.main;
            SetBackground();
        }
        
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

    public void PauseGame(bool paused)
    {
        if (!isPaused)
        {
            isPaused = paused;
            print("Paused = " + isPaused);
            StopAllCoroutines();
        }
        else
        {
            isPaused = paused;
            print("Paused = " + isPaused);
            StartCoroutine(CountDownLevel());
        }
    }
}
