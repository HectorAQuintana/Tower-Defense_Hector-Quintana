using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnGameState : MonoBehaviour
{
    [SerializeField]
    protected GameStateSO gameState;
    [SerializeField]
    private GameStateSO.GameState stateToActivate;

    protected void OnDestroy()
    {
        gameState.OnGameStateChanged -= SetActive;
    }

    private void Start()
    {
        gameState.OnGameStateChanged += SetActive;
    }

    private void SetActive()
    {
        switch(stateToActivate)
        {
            case GameStateSO.GameState.Building:
                gameObject.SetActive(gameState.IsOnBuildingMode);
                break;
            case GameStateSO.GameState.Battling:
                gameObject.SetActive(gameState.IsOnBattlingMode);
                break;
        }
    }
}
