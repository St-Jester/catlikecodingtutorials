using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour {
    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;
    public ObjectPool pool { get; set; }
    public void ReturnToPool()
    {
        if (pool)
            pool.AddObject(this);
        else
            Destroy(gameObject);
    }
    public T GetPooledInstance<T>() where T : PooledObject
    {
        if(!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        return (T)poolInstanceForPrefab.GetObject();
    }
   
}
