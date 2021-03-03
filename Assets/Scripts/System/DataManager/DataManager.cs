using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    //public PlayerStatistics savedPlayerData = new PlayerStatistics();

    // Variables for summary screen
    // These reset every round;
    [SerializeField] private ScriptableFloat actualMoney;
    [SerializeField] private ScriptableFloat expectedMoney;
    [SerializeField] private ScriptableFloat customersServed;


    [SerializeField] private float totalMoney;

    public float PlayerTotalMoney
    {
        get { return totalMoney; }
        set { totalMoney = value; }
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //PlayerTotalMoney = 200;

        // Resets the summary screen variables every new round
        actualMoney.Value = 0;
        expectedMoney.Value = 0;
        customersServed.Value = 0;
    }
}

