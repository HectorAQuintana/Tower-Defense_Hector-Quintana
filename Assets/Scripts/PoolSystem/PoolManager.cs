using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    private static Dictionary<string, PoolObject> pools = new Dictionary<string, PoolObject>();
    public static Dictionary<string, PoolObject> Pools => pools;

    public static GameObject InitializePool(GameObject prefab, int poolMaxSize)
    {
        if (!pools.ContainsKey(prefab.name))
        {
            PoolObject newPool = new PoolObject(prefab, poolMaxSize);
            pools.Add(prefab.name, newPool);
        }

        return pools[prefab.name].Get;
    }
}
