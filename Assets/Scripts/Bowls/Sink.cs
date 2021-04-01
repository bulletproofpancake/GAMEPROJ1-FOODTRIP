using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sink : MonoBehaviour
{
    public static Sink sink;
    private TextMeshProUGUI _btnText;
    private List<GameObject> _bowls;
    private bool _isBtnTextNotNull;

    private void Start()
    {
        _isBtnTextNotNull = _btnText!=null;
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

    void GarbageChecker()
    {
        //maximum capacity = 10
        //bowls.Length == 10
        //public boolean Customer, full na ang garbage.
        //Di na pwedeng tumanggap
        //CleanGarbage(_bowls);
        
        // foreach (var bowl in _bowls)
        // {
        //     Wash(bowl);
        // }
    }
}
