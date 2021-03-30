using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        bowl.GetComponent<Bowl>().isDirty = false;
        _bowls.Remove(bowl);
        yield return new WaitForSeconds(bowl.GetComponent<Bowl>().currentWashTime);       
        BowlSpawner.spawner.AddBowl(bowl);
    }
    
    public void AddBowl(GameObject bowl)
    {
        _bowls.Add(bowl);
    }
}
