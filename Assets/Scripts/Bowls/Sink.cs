using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sink : MonoBehaviour
{
    public static Sink sink;
    private TextMeshProUGUI _btnText;
    [SerializeField] private List<GameObject> _bowls;
    private bool _isBtnTextNotNull;
    private Button button;
    private Image image;
    [SerializeField] private Sprite empty,half,full;
    [SerializeField] private int maxCapacity;
    
    private void Start()
    {
        _isBtnTextNotNull = _btnText!=null;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    [SerializeField] private Image fillImage;
    private bool isWashing;
    private float timer;

    private void Awake()
    {
        sink = GetComponent<Sink>();
        _bowls = new List<GameObject>();
        _btnText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        if(_isBtnTextNotNull)
            _btnText.text = $"{name}: {_bowls.Count}";
        WashingIndicator();

        if (Pause.isPaused == true)
            button.interactable = false;
        else if (Pause.isPaused == false)
            button.interactable = true;
        
        if(ShiftManager.Instance.cart.Type == CartType.Tusoktusok)
            GarbageChecker();
    }

    public void WashBowl()
    {
        if(_bowls.Count > 0)
            StartCoroutine(Wash(_bowls[_bowls.Count - 1]));
        else
            Debug.LogWarning("No dishes left to wash");
        
    }
    
    private IEnumerator Wash(GameObject bowl)
    {
        isWashing = true;
        bowl.GetComponent<Bowl>().isDirty = false;
        _bowls.Remove(bowl);
        yield return new WaitForSeconds(bowl.GetComponent<Bowl>().currentWashTime);
        isWashing = false;
        BowlSpawner.spawner.AddBowl(bowl);
    }
    
    public void AddBowl(GameObject bowl)
    {
        _bowls.Add(bowl);
    }

    private void WashingIndicator()
    {
        if (_bowls.Count > 0)
        {
            if (isWashing == true)
            {
                timer = _bowls[_bowls.Count - 1].GetComponent<Bowl>().currentWashTime;
                fillImage.fillAmount += 1.0f / timer * Time.deltaTime;
            }
            else if (isWashing == false)
            {
                timer = 0;
                fillImage.fillAmount = timer;
            }
        }
        else
            fillImage.fillAmount = 0;
    }

    private void GarbageChecker()
    {
        if (_bowls.Count < maxCapacity / 2)
        {
            image.sprite = empty;
        }
        else if (_bowls.Count >= maxCapacity / 2 && _bowls.Count < maxCapacity)
        {
            image.sprite = half;
        }
        else
        {
            image.sprite = full;
            GameManager.Instance.garbageFull = true;
        }
    }

    public void CleanGarbage()
    {
        GameManager.Instance.garbageFull = false;
        foreach (var bowl in _bowls)
        {
            bowl.GetComponent<Bowl>().isDirty = false;
            BowlSpawner.spawner.AddBowl(bowl);
        }
        StartCoroutine(GarbageIndicator());
        print(_bowls.Count);
    }

    IEnumerator GarbageIndicator()
    {
        isWashing = true;
        yield return new WaitForSeconds(_bowls[0].GetComponent<Bowl>().currentWashTime);
        isWashing = false;
        _bowls.Clear();
    }
    
}
