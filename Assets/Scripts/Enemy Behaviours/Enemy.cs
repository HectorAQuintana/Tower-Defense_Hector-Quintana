using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemySO enemySO;

    private EnemySO.State state = EnemySO.State.Alive;
    private WaypointsSystem waypointsSystem;
    private int waypointIndex = 0;
    private Vector3 positionToGo;
    private int currentHealth;
    private float currentSpeed;

    public UnityAction OnEnemyDeath;

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
    }

    private void OnEnable()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        state = EnemySO.State.Alive;
        transform.position = waypointsSystem.GetWaypointPosition(0);
        waypointIndex = 0;
        UpdatePositionToGo();
        currentHealth = enemySO.Health;
        currentSpeed = enemySO.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsAlive) { return; }
        
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

        return distance < 0.1f;
    }

    private void UpdatePositionToGo()
    {
        waypointIndex++;
        positionToGo = waypointsSystem.GetWaypointPosition(waypointIndex);
    }

    private void EnemyDefeated()
    {
        OnEnemyDeath.Invoke();
        state = EnemySO.State.Death;
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

    public void EnemyReachBase()
    {
        EnemyDefeated();
    }

    public int GetCurrentHealth => currentHealth;
    public int GetMaxHealth => enemySO.Health;
    public bool IsAlive => state == EnemySO.State.Alive;
}
