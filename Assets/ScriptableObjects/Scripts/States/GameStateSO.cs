using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateScriptableObject", menuName = "ScriptableObjects/GameState")]
public class GameStateSO : ScriptableObject
{
    private enum GameState
    {
        Building,
        Battling
    }

    private GameState currentState = GameState.Building;

    private void OnEnable()
    {
        if (IsOnBuildingMode) { return; }

        currentState = GameState.Building;
    }

    private void OnDisable()
    {
        if (IsOnBuildingMode) { return; }

        currentState = GameState.Building;
    }

    public Action OnGameStateChanged;
    public bool IsOnBuildingMode => currentState == GameState.Building;
    public bool IsOnBattlingMode => currentState == GameState.Battling;

    public void ChangeStateToBuilding()
    {
        if(IsOnBuildingMode) { return; }

        currentState = GameState.Building;
        OnGameStateChanged?.Invoke();
    }

    public void ChangeStateToBattling()
    {
        if (IsOnBattlingMode) { return; }

        currentState = GameState.Battling;
        OnGameStateChanged?.Invoke();
    }
}
