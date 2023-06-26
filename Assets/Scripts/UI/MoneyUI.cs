using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        playerStats.OnMoneyChanged += UpdateText;
    }

    private void OnDisable()
    {
        playerStats.OnMoneyChanged -= UpdateText;
    }

    private void UpdateText()
    {
        moneyText.text = playerStats.GetCurrentMoney.ToString();
    }
}
