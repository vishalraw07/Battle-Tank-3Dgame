using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    /*
        Generic Class for Object Pool. 
    */
    public class GenericObjectPool<T>
    {
        private Transform parentTransform;
        private GameObject objectPrefab;
        
        public Queue<T> objectPool = new Queue<T>();

        /* 
            Generates ObjectPool by taking Prefab, PoolCount & the parent transform as parameters.
            Enqueues the disabled gameobjects in ObjectPool queue.
            Parameters : 
            - objPrefab : Prefab for object to be instantiated.
            - poolCount : Initial Size of Object Pool.
            - parentTF  : Transform of Parent object for Instantiate().
        */
        public void GeneratePool(GameObject objPrefab, int poolCount, Transform parentTF) {
            parentTransform = parentTF;
            objectPrefab = objPrefab;
            for (int i = 0; i < poolCount; i++) {
                GameObject item = GameObject.Instantiate(objectPrefab, parentTransform);
                item.SetActive(false);
                T poolItem = item.GetComponent<T>();
                objectPool.Enqueue(poolItem);
            }
        }

        /*
            Gets a item from the ObjectPool. If the queue is empty, it creates a new object and returns it.
        */
        public T GetItem() {
            if (objectPool.Count > 0) {
                return objectPool.Dequeue();
            }
            else {
                GameObject item = GameObject.Instantiate(objectPrefab, parentTransform);
                item.SetActive(false);
                T poolItem = item.GetComponent<T>();
                objectPool.Enqueue(poolItem);
                return objectPool.Dequeue();
            }
        }

        /*
            Returns the item back to ObjectPool. Adds it to the queue.
            Parameters : 
            - poolItem : Returned Item (already disabled.)
        */
        public void ReturnItem(T poolItem) {
            objectPool.Enqueue(poolItem);
        }
    }

}

