using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool<T> : IPool where T : IPoolable, new()
    { 
        
        List<T> objectPool = new List<T>();
        int objectsInPool, maxPool = 100;
        public ObjectPool()
        {
            // for (int i = 0; i < maxPool / 2; i++)
            // {
            //     T poolObject = new T();
            //     objectPool.Add(poolObject);
            //     poolObject.Reset();
            // }
        }

        public T GetFromPool<BT>() where BT : T, new()
        {
             //Debug.Log("ask from pool count of pool"+objectPool.Count);
            T poolObject=default(T);
            if (objectPool.Count == 0 && objectsInPool <= maxPool)
            {
                objectsInPool++;
                Debug.Log("creating new object");
                poolObject = (T)new BT();
            }
            else
            {
                foreach (T obj in objectPool)
                {
                    if (obj is BT)
                    {
                        poolObject = obj;
                        objectPool.Remove(poolObject);
                        Debug.Log("object from pool");
                        break;
                    }
                    else
                    {
                        objectsInPool++;
                        Debug.Log("creating new object");
                        poolObject = (T)new BT();
                    }
                }
            }
            return poolObject;
        }
        public void ReturnToPool(T poolObject)
        {
            objectPool.Add(poolObject);
            poolObject.Reset();
            Debug.Log("back to pool count of pool"+objectPool.Count);
        }

    }
}