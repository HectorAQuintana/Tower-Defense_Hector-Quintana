using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnGameState : MonoBehaviour
{
    [SerializeField]
    protected GameStateSO gameState;
    [SerializeField]
    private GameStateSO.GameState stateToActivate;

    protected virtual void OnEnable()
    {
        gameState.OnGameStateChanged += SetActive;
    }

    void OnDestroy()
    {
        gameState.OnGameStateChanged -= SetActive;
    }

    private void Start()
    {
        SetActive();
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
