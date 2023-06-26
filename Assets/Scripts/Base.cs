using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private GameStateSO gameState;

    private void ResetHealth()
    {
        playerStats.ResetHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().EnemyReachBase();
        }
    }
}
