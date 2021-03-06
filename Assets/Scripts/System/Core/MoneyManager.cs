﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    public float expectedMoney;
    public float totalMoney;
    public float roundMoney;
    public float currentMoney;
    public float previousMoney;

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
        previousMoney = roundMoney;
        currentMoney = 0;
        roundMoney = 0;
        expectedMoney = 0;
    }

    public void Spend(float cost)
    {
        totalMoney -= cost;
    }
}