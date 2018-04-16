using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    PooledObject prefab;
    List<PooledObject> AvaliableObj = new List<PooledObject>();
    public PooledObject GetObject()
    {
        PooledObject obj;
         int LastIndex = AvaliableObj.Count - 1;
        if (LastIndex >= 0)
        {
            obj = AvaliableObj[LastIndex];
            AvaliableObj.RemoveAt(LastIndex);
            obj.gameObject.SetActive(true);    
        }
        else
        {
            obj = Instantiate(prefab);
            obj.transform.SetParent(transform, false);
            obj.pool = this;
           
        }
        return obj;
    }
    public void AddObject(PooledObject o)
    {
        o.gameObject.SetActive(false);
        AvaliableObj.Add(o);
    }
    public static ObjectPool GetPool(PooledObject prefab)
    {
        GameObject obj;
        ObjectPool pool;
        if (Application.isEditor)
        {
            obj = GameObject.Find(prefab.name + "Pool");
            DontDestroyOnLoad(obj);
            if(obj)
            {
                pool = obj.GetComponent<ObjectPool>();
                if (pool)
                    return pool;
            }
        }
        obj = new GameObject(prefab.name + "Pool"); 
        pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }
}
