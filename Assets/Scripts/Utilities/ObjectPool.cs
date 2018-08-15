using UnityEngine;
using System.Collections.Generic;

namespace RogueSharpTutorial.Utilities
{
    /// <summary>
    /// Object pool code is entirely based on tutorial code from Catlike Coding: https://catlikecoding.com/unity/tutorials/object-management/reusing-objects/ 
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public class ObjectPool : MonoBehaviour
    {
        private PooledObject        prefab;
        private List<PooledObject>  availableObjects = new List<PooledObject>();

        /// <summary>
        /// Create if necessary and return the Object Pool.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static ObjectPool GetPool(PooledObject prefab)
        {
            GameObject obj;
            ObjectPool pool;
            if (Application.isEditor)
            {
                obj = GameObject.Find(prefab.name + " Pool");
                if (obj)
                {
                    pool = obj.GetComponent<ObjectPool>();
                    if (pool)
                    {
                        return pool;
                    }
                }
            }
            obj = new GameObject(prefab.name + " Pool");
            DontDestroyOnLoad(obj);
            pool = obj.AddComponent<ObjectPool>();
            pool.prefab = prefab;
            return pool;
        }

        /// <summary>
        /// Get a pooled obect from the Object Pool.
        /// </summary>
        /// <returns></returns>
        public PooledObject GetObject()
        {
            PooledObject obj;
            int lastAvailableIndex = availableObjects.Count - 1;
            if (lastAvailableIndex >= 0)
            {
                obj = availableObjects[lastAvailableIndex];
                availableObjects.RemoveAt(lastAvailableIndex);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Instantiate<PooledObject>(prefab);
                obj.transform.SetParent(transform, false);
                obj.Pool = this;
            }
            return obj;
        }

        /// <summary>
        /// Set pooled object inactive and return to the Object Pool.
        /// </summary>
        /// <param name="obj"></param>
        public void AddObject(PooledObject obj)
        {
            obj.gameObject.SetActive(false);
            availableObjects.Add(obj);
        }
    }
}