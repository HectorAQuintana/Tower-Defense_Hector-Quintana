using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemySO enemySO;
    [SerializeField]
    private HordeEventsSO hordeEvent;
    [SerializeField]
    private PlayerStatsSO playerStats;
    [SerializeField]
    private Slider healthBar;

    private EnemySO.State state = EnemySO.State.Alive;
    private WaypointsSystem waypointsSystem;
    private int waypointIndex = 0;
    private Vector3 positionToGo;
    private int currentHealth;
    private float currentSpeed;

    public UnityAction OnEnemyDeath;

    // Start is called before the first frame update
    void Awake()
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
        hordeEvent.OnEnemySpawned.Invoke();
        UpdateHealthBar();
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

    private void UpdateHealthBar()
    {
        healthBar.value = (float)currentHealth / (float)enemySO.Health;
    }

    private void EnemyDefeated(bool byPlayer)
    {
        OnEnemyDeath?.Invoke();
        state = EnemySO.State.Death;
        hordeEvent.OnEnemyDestroyed.Invoke();
        gameObject.SetActive(false);

        if(byPlayer)
        {
            playerStats.AddMoney(enemySO.MoneyDrop);
        }
    }

    public void RecieveDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            EnemyDefeated(true);
        }
    }

    public void EnemyReachBase()
    {
        playerStats.RecieveDamage(enemySO.Damage);
        EnemyDefeated(false);
    }

    public int GetCurrentHealth => currentHealth;
    public int GetMaxHealth => enemySO.Health;
    public bool IsAlive => state == EnemySO.State.Alive;
}
