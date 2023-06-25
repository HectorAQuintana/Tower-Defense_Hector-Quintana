using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuyButton : SetActiveOnGameState
{
    [SerializeField]
    private PlayerStateSO playerState;
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

    protected override void OnEnable()
    {
        base.OnEnable();
        playerState.OnPlayerStateChange += SetButtonInteraction;
    }

    void OnDisable()
    {
        playerState.OnPlayerStateChange -= SetButtonInteraction;
    }

    private void SetButtonInteraction()
    {
        button.interactable = playerState.IsIdle ? true : false;
    }
}