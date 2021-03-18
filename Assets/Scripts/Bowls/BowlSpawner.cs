using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BowlSpawner : Singleton<BowlSpawner>
{
    private TextMeshProUGUI _btnText;
    public List<GameObject> _bowls = new List<GameObject>();
    
    private void Start()
    {
        //FillBowl();
        _btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void FillBowl()
    {
        var bowls = FindObjectsOfType<Bowl>();
        foreach (var bowl in bowls)
        {
            _bowls.Add(bowl.gameObject);
        }
    }
    
    void Update()
    {
        _btnText.text = $"{name}: {_bowls.Count}";
    }

    public void AddBowl(GameObject bowl)
    {
        _bowls.Add(bowl);
    }

    public void RemoveBowl(GameObject bowl)
    {
        _bowls.Remove(bowl);
    }
    
    public void SpawnBowl()
    {
        //SpawnManager.Instance.SpawnBowl(_bowls[_bowls.Count-1]);
    }
}
