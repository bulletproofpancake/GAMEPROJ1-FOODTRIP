using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BowlSpawner : MonoBehaviour
{
    public static BowlSpawner spawner;
    private TextMeshProUGUI _btnText;
    public List<GameObject> _bowls;
    private bool _isBtnTextNotNull;
    private Button button;

    private void Start()
    {
        _isBtnTextNotNull = _btnText!=null;
        button = GetComponent<Button>();
    }

    private void Awake()
    {
        spawner = GetComponent<BowlSpawner>();
        _btnText = GetComponentInChildren<TextMeshProUGUI>();
        _bowls = new List<GameObject>();
    }

    private void Update()
    {
        if(_isBtnTextNotNull)
            _btnText.text = $"{name}: {_bowls.Count}";

        if (Pause.isPaused == true)
            button.interactable = false;
        else if (Pause.isPaused == false)
            button.interactable = true;
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
        SpawnManager.spawner.SpawnBowl(_bowls[_bowls.Count-1]);
    }
}
