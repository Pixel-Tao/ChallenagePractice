using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private const int defaultCapacity = 50;
    private const int maxSize = 300;

    Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

    public ObjectPool CreatePool(string poolName, GameObject prefab)
    {
        if (pools.ContainsKey(poolName))
        {
            Debug.LogWarning("Pool with name " + poolName + " already exists");
            return null;
        }

        var pool = new ObjectPool(poolName, prefab, transform, defaultCapacity, maxSize);
        pools.Add(poolName, pool);

        return pool;
    }
    
    public GameObject Spawn(GameObject prefab, Vector3 pos)
    {
        if (pools.TryGetValue(prefab.name, out ObjectPool pool) == false)
        {
            pool = CreatePool(prefab.name, prefab);
        }

        GameObject go = pool.Spawn();
        go.transform.position = pos;
        return go;
    }

    public GameObject Spawn(string name, Vector3 pos)
    {
        if (pools.TryGetValue(name, out ObjectPool pool) == false)
        {
            
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/{name}.prefab");
            pool = CreatePool(name, prefab);
        }

        GameObject go = pool.Spawn();
        go.transform.position = pos;
        return go;
    }

    public void Despawn(GameObject go)
    {
        if (pools.TryGetValue(go.name, out ObjectPool pool) == false)
        {
            Debug.LogWarning("Pool with name " + go.name + " does not exist");
            Destroy(go);
            return;
        }

        pool.Despawn(go);
    }
}
