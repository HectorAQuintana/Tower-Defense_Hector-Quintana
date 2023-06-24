using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private TowerSO towerSO;
    [SerializeField]
    private SphereCollider sphereCollider;

    private int level = 1;
    private int currentDamage;
    private float currentFireRate;
    private int currentPrice;

    public int GetCurrentPrice => currentPrice;

    // Start is called before the first frame update
    void Start()
    {
        if(!towerSO)
        {
            Debug.LogError($"No TowerSO assigned to {gameObject.name}'s Tower Script!");

            return;
        }

        if (!sphereCollider)
        {
            Debug.LogError($"No SphereCollider assigned to {gameObject.name}'s Tower Script!");

            return;
        }

        sphereCollider.radius = towerSO.Range;

        UpdateTowerStats();
    }

    private void UpdateTowerStats()
    {
        currentDamage = Mathf.FloorToInt(UpgradeStat(towerSO.Damage));
        currentFireRate = UpgradeStat(towerSO.FireRate);
        currentPrice = Mathf.FloorToInt(UpgradeStat(towerSO.Price));
    }

    private float UpgradeStat(float stat)
    {
        return (stat + ((level - 1) * towerSO.Multiplier * stat));
    }

    public void LevelUp()
    {
        level++;
        UpdateTowerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
