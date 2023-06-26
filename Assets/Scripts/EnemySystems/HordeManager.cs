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
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField]
    private int hordeQuantity = 10;
    [SerializeField]
    private float spawnCooldwon = 3;

    private int hordeSpawned = 0;
    private int hordeAlived = 0;

    private void OnEnable()
    {
        gameState.OnGameStateChanged += StartHorde;
        hordeEvents.OnEnemySpawned += EnemySpawned;
        hordeEvents.OnEnemyDestroyed += EnemyDestroyed;
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

        hordeSpawned = 0;
        StartCoroutine(HordeSpawner());
    }

    private IEnumerator HordeSpawner()
    {
        while(hordeSpawned < hordeQuantity)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Count);
            SpawnMonster(enemyPrefabs[randomEnemy]);

            yield return new WaitForSeconds(spawnCooldwon);
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
        hordeAlived++;
    }

    private void EnemyDestroyed()
    {
        hordeAlived--;
        if(hordeAlived <= 0)
        {
            hordeAlived = 0;
            gameState.ChangeStateToBuilding();
        }
    }
}
