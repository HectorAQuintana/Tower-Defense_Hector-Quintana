using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemySO enemySO;

    private WaypointsSystem waypointsSystem;
    private int waypointIndex = 0;
    private Vector3 positionToGo;

    private int currentHealth;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        waypointsSystem = FindObjectOfType<WaypointsSystem>();

        if(!enemySO)
        {
            Debug.LogError($"EnemySO not assigned to {gameObject.name}'s Enemy Script!");
            return;
        }

        if (waypointsSystem == null)
        {
            Debug.LogError($"Couldn't find WaypointSystem on current Scene!");
            return;
        }

        transform.position = waypointsSystem.GetWaypointPosition(0);

        UpdatePositionToGo();

        currentHealth = enemySO.Health;
        currentSpeed = enemySO.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(ReachedPosition())
        {
            UpdatePositionToGo();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionToGo, currentSpeed * Time.deltaTime);
    }

    private bool ReachedPosition()
    {
        float distance = Vector3.Distance(transform.position, positionToGo);

        return distance < 1;
    }

    private void UpdatePositionToGo()
    {
        waypointIndex++;
        positionToGo = waypointsSystem.GetWaypointPosition(waypointIndex);
    }

    private void EnemyDefeated()
    {
        gameObject.SetActive(false);
    }

    public void RecieveDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            EnemyDefeated();
        }
    }

    public int GetCurrentHealth => currentHealth;
    public int GetMaxHealth => enemySO.Health;
}
