using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : SetActiveOnGameState
{
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private Slider slider;

    protected override void OnEnable()
    {
        base.OnEnable();
        playerStats.OnBaseHealthChange += UpdateSlider;
    }

    void OnDisable()
    {
        playerStats.OnBaseHealthChange -= UpdateSlider;
    }

    private void UpdateSlider()
    {
        slider.value = (float)playerStats.GetCurrentHealth / (float)playerStats.GetInitialHealth;
    }
}
