using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerStatsScriptableObject", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [field: SerializeField]
    private int initialHealth = 500;
    [field: SerializeField]
    private int initialMoney = 200;

    private int currentHealth;
    private int currentMoney;

    public Action BaseHealthChange;
    public Action OnBaseDestroyed;
    public Action OnMoneyChanged;

    private void OnEnable()
    {
        currentHealth = initialHealth;
        currentMoney = initialMoney;
    }

    public int GetCurrentHealth => currentHealth;
    public int GetCurrentMoney => currentMoney;

    public void RecieveDamage(int damage)
    {
        if(currentHealth <= 0) { return; }

        currentHealth -= damage;
        BaseHealthChange?.Invoke();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnBaseDestroyed?.Invoke();
        }
    }

    public void ResetHealth()
    {
        currentHealth = initialHealth;
        BaseHealthChange?.Invoke();
    }

    public void SetStatsBeforeHorde()
    {
        currentHealth = initialHealth;
        OnMoneyChanged?.Invoke();
        BaseHealthChange?.Invoke();
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
        OnMoneyChanged?.Invoke();
    }

    public void RemoveMoney(int money)
    {
        currentMoney -= money;
        OnMoneyChanged?.Invoke();
    }
}
