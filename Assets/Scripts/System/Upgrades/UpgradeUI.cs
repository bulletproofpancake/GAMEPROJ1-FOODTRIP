using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Update()
    {
        moneyText.text = $"{MoneyManager.Instance.totalMoney:F}";
    }
}
