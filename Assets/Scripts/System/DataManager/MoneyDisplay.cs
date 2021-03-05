using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Update()
    {
        moneyText.text = DataManager.Instance.PlayerTotalMoney.ToString();
    }
}
