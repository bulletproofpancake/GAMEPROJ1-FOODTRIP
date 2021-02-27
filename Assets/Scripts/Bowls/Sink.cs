using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sink : Singleton<Sink>
{
    private TextMeshProUGUI _btnText;
    private List<GameObject> _bowls;

    protected override void Awake()
    {
        base.Awake();
        _bowls = new List<GameObject>();
        _btnText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    private void Update()
    {
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
        yield return new WaitForSeconds(bowl.GetComponent<Bowl>().washTime);       
        BowlSpawner.Instance.AddBowl(bowl);
    }
    
    public void AddBowl(GameObject bowl)
    {
        _bowls.Add(bowl);
    }
}
