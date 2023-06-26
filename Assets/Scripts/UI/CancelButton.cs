using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : SetActiveOnGameState
{
    [SerializeField]
    private PlayerStateSO playerState;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(delegate () { playerState.SetPlayerToIdle(); });
    }

    void OnEnable()
    {
        playerState.OnPlayerStateChange += SetButtonInteraction;
        SetButtonInteraction();
    }

    void OnDisable()
    {
        playerState.OnPlayerStateChange -= SetButtonInteraction;
    }

    private void SetButtonInteraction()
    {
        gameObject.SetActive(playerState.IsBuilding);
    }
}
