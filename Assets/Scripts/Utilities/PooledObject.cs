using UnityEngine;

namespace RogueSharpTutorial.Utilities
{
    /// <summary>
    /// Object pool code is entirely based on tutorial code from Catlike Coding: https://catlikecoding.com/unity/tutorials/object-management/reusing-objects/ 
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public class PooledObject : MonoBehaviour
    {
        public ObjectPool Pool { get; set; }

        [System.NonSerialized] private ObjectPool poolInstanceForPrefab;

        /// <summary>
        /// Return the object back to the pool if available or destroy if no pool.
        /// </summary>
        public virtual void ReturnToPool()
        {
            if (Pool)
            {
                Pool.AddObject(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Get an instance of the Pooled Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPooledInstance<T>() where T : PooledObject
        {
            if (!poolInstanceForPrefab)
            {
                poolInstanceForPrefab = ObjectPool.GetPool(this);
            }
            return (T)poolInstanceForPrefab.GetObject();
        }
    }
}