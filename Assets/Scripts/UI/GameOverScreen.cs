using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private GameObject gameOverScreen;

    private void OnEnable()
    {
        playerStats.OnBaseDestroyed += ActivateScreen;
    }

    private void OnDisable()
    {
        playerStats.OnBaseDestroyed -= ActivateScreen;
    }

    private void ActivateScreen()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
