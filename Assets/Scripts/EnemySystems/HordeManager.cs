using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    [SerializeField]
    private GameStateSO gameState;
    [SerializeField]
    private HordeEventsSO hordeEvents;
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField]
    private int hordeQuantity = 10;
    [SerializeField]
    private float spawnCooldwon = 3;

    private int hordeLevel = 1;
    private int hordeSpawned = 0;
    private int hordeAlived = 0;

    private int currentHordeQuantity;
    private float currentSpawnCooldown;

    private void OnEnable()
    {
        gameState.OnGameStateChanged += StartHorde;
        hordeEvents.OnEnemySpawned += EnemySpawned;
        hordeEvents.OnEnemyDestroyed += EnemyDestroyed;

        currentHordeQuantity = hordeLevel * hordeQuantity;
        currentSpawnCooldown = spawnCooldwon / hordeLevel;
    }

    private void OnDisable()
    {
        gameState.OnGameStateChanged -= StartHorde;
        hordeEvents.OnEnemySpawned -= EnemySpawned;
        hordeEvents.OnEnemyDestroyed -= EnemyDestroyed;
    }

    private void StartHorde()
    {
        if(gameState.IsOnBuildingMode) { return; }

        playerStats.ResetHealth();

        currentHordeQuantity = hordeLevel * hordeQuantity;
        currentSpawnCooldown = spawnCooldwon / hordeLevel;
        hordeSpawned = 0;
        hordeAlived = currentHordeQuantity;

        StartCoroutine(HordeSpawner());
    }

    private IEnumerator HordeSpawner()
    {
        while(hordeSpawned < currentHordeQuantity)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Count);
            SpawnMonster(enemyPrefabs[randomEnemy]);

            yield return new WaitForSeconds(currentSpawnCooldown);
        }
    }

    private void SpawnMonster(GameObject enemy)
    {
        if (!PoolManager.Pools.ContainsKey(enemy.name))
        {
            enemy = PoolManager.InitializePool(enemy, 20);
        }
        else
        {
            enemy = PoolManager.Pools[enemy.name].Get;
        };
    }

    private void EnemySpawned()
    {
        hordeSpawned++;
    }

    private void EnemyDestroyed()
    {
        hordeAlived--;
        if(hordeAlived <= 0)
        {
            hordeLevel++;

            playerStats.ResetHealth();
            gameState.ChangeStateToBuilding();
            
        }
    }
}
