using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private TowerSO towerSO;
    [SerializeField]
    private SphereCollider sphereCollider;
    [SerializeField]
    private Transform projectileSpawnTransfomr;

    private int level = 1;
    private int currentDamage;
    private float currentFireRate;
    private int currentPrice;
    private Enemy enemieInRange;
    private bool canShoot = true;
    public int GetCurrentPrice => currentPrice;
    public TowerSO TowerScriptableObject => towerSO;

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

    // Update is called once per frame
    void Update()
    {
        AimAtEnemy();
    }

    private void AimAtEnemy()
    {
        if (enemieInRange == null) { return; }

        Vector3 enemiePosition = enemieInRange.transform.position;
        Vector3 lookAtPosition = new Vector3(enemiePosition.x, transform.position.y, enemiePosition.z);

        transform.LookAt(lookAtPosition);

        if(canShoot)
            ShootProjectilce();
    }

    private IEnumerator TowerCooldown()
    {
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    private void ShootProjectilce()
    {
        canShoot = false;

        GameObject projectile;

        if(!PoolManager.Pools.ContainsKey(towerSO.ProjectilePrefab.name))
        {
            projectile = PoolManager.InitializePool(towerSO.ProjectilePrefab, 100);
        }
        else
        {
            projectile = PoolManager.Pools[towerSO.ProjectilePrefab.name].Get;
        }

        projectile.transform.position = projectileSpawnTransfomr.position;
        projectile.transform.rotation = projectileSpawnTransfomr.rotation;

        projectile.GetComponent<Projectile>().ShootProjectile(currentDamage);

        StartCoroutine(TowerCooldown());
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && enemieInRange == null)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            enemieInRange = newEnemy;
            newEnemy.OnEnemyDeath += RemoveEnemy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemieInRange == other.GetComponent<Enemy>())
        {
            RemoveEnemy();
        }
    }

    private void RemoveEnemy()
    {
        enemieInRange.OnEnemyDeath -= RemoveEnemy;
        enemieInRange = null;
    }

    public void LevelUp()
    {
        level++;
        UpdateTowerStats();
    }
}
