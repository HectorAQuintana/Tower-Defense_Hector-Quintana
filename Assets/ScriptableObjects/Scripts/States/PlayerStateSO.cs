using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateScriptableObject", menuName = "ScriptableObjects/PlayerState")]

public class PlayerStateSO : ScriptableObject
{
    private enum PlayerState
    {
        Idle,
        Building
    }

    public Action OnPlayerStateChange;

    private PlayerState playerState = PlayerState.Idle;

    private void OnEnable()
    {
        if (IsIdle) { return; }

        playerState = PlayerState.Idle;
    }

    private void OnDisable()
    {
        if (IsIdle) { return; }

        playerState = PlayerState.Idle;
    }

    public bool IsIdle => playerState == PlayerState.Idle;
    public bool IsBuilding => playerState == PlayerState.Building;

    public void SetPlayerToIdle()
    {
        if (IsIdle) { return; }

        playerState = PlayerState.Idle;
        OnPlayerStateChange?.Invoke();
    }

    public void SetPlayerToBuilding()
    {
        if (IsBuilding) { return; }

        playerState = PlayerState.Building;
        OnPlayerStateChange?.Invoke();
    }
}
