using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private float totalMoney;

    public float PlayerTotalMoney
    {
        get { return totalMoney; }
        set { totalMoney = value; }
    }


}

