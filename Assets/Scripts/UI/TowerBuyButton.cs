using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuyButton : SetActiveOnGameState
{
    [SerializeField]
    private PlayerStateSO playerState;
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private GameObject towerPrefab;

    private Button button;
    private TowerSpawn towerSpawnManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        towerSpawnManager = FindObjectOfType<TowerSpawn>();

        button.onClick.AddListener(delegate () { towerSpawnManager.TowerSelected(towerPrefab); });
    }

    void OnEnable()
    {
        playerState.OnPlayerStateChange += SetButtonInteraction;
        playerStats.OnMoneyChanged += SetButtonInteraction;
        SetButtonInteraction();
    }

    void OnDisable()
    {
        playerState.OnPlayerStateChange -= SetButtonInteraction;
        playerStats.OnMoneyChanged -= SetButtonInteraction;
    }

    private void SetButtonInteraction()
    {
        button.interactable = playerState.IsIdle && playerStats.GetCurrentMoney >= towerPrefab.GetComponent<Tower>().TowerScriptableObject.Price;
    }
}
