using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolObject
{
    private GameObject prefab;
    private ObjectPool<GameObject> pool;

    public PoolObject(GameObject objToPool, int maxPoolSize)
    {
        prefab = objToPool;
        pool = new ObjectPool<GameObject>(CreatePooledObject, OnGet, OnRelease, OnDestroy, true, maxPoolSize);
    }

    private GameObject CreatePooledObject()
    {
        GameObject newObject = Object.Instantiate(prefab);
        newObject.name = prefab.name;
        newObject.SetActive(true);
        return newObject;
    }

    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroy(GameObject obj)
    {
        Object.Destroy(obj);
    }

    public GameObject Get => pool.Get();

    public void Release(GameObject obj)
    {
        pool.Release(obj);
    }
}
