using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartHordeButton : SetActiveOnGameState
{
    [SerializeField]
    private PlayerStateSO playerState;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(StartHorde);
    }

    private void StartHorde()
    {
        gameState.ChangeStateToBattling();
        playerState.SetPlayerToIdle();
    }

    void OnEnable()
    {
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
