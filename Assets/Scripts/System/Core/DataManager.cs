using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public float totalMoney;
    public float roundMoney;
    public float currentMoney;

    public void Collect(float payment)
    {
        currentMoney += payment;
    }

    public void Earn()
    {
        roundMoney += currentMoney;
        totalMoney += roundMoney;
    }

    public void EndRound()
    {
        currentMoney = 0;
        roundMoney = 0;
    }
    
}
