using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float aliveTime = 2;

    private int damage = 0;
    private bool isReady = false;

    public void ShootProjectile(int bulletDamage)
    {
        damage = bulletDamage;
        isReady = true;
        StartCoroutine(ProjectileTimer());
    }

    private void Update()
    {
        if(!isReady) { return; }

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    IEnumerator ProjectileTimer()
    {
        yield return new WaitForSeconds(aliveTime);

        ReturnProjectile();
    }

    private void ReturnProjectile()
    {
        if(!isReady) { return; }

        isReady = false;
        PoolManager.Pools[name].Release(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy")) { return; }

        other.GetComponent<Enemy>().RecieveDamage(damage);

        ReturnProjectile();
    }
}
