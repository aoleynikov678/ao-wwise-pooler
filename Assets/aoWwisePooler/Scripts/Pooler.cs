using System.Collections.Generic;
using UnityEngine;

namespace ao.wwisepooler
{
    public class Pooler : MonoBehaviour
    {
        [SerializeField] private List<Pool> pools = new List<Pool>();
        
        private readonly Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

        #region Singleton
        
        private static Pooler instance;
        public static Pooler Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("Pooler instance is null");
                }

                return instance;
            }

            private set { instance = value; }
        }
        
        #endregion

        private void Awake()
        {
            Instance = this;

            foreach (var pool in pools)
            {
                var objs = GeneratePool(pool);

                if (!PoolExists(pool.id))
                {
                    pool.SetObjects(objs);
                    poolDict.Add(pool.id, pool);
                }
            }
        }

        private List<Poolable> GeneratePool(Pool poolDescriptor)
        {
            var p = new List<Poolable>();
            
            for (int i = 0; i < poolDescriptor.size; i++)
            {
                CreateAndAdd(poolDescriptor, p, false);
            }

            return p;
        }

        private Poolable CreateAndAdd(Pool poolDescriptor, List<Poolable> p, bool setActive)
        {
            var poolable = Instantiate(poolDescriptor.prefab, transform, true);
            poolable.gameObject.SetActive(setActive);
            poolable.SetPooler(this, poolDescriptor);
                
            p.Add(poolable);

            return poolable;
        }
        
        private bool PoolExists(string poolID)
        {
            return poolDict.ContainsKey(poolID);
        }

        public T RequestFromPool<T>(string poolID) where T : Poolable
        {
            if (!PoolExists(poolID))
            {
                Debug.LogError($"No pool with id {poolID}");
                return null;
            }
            
            foreach (var poolable in poolDict[poolID].ObjectsInPool)
            {
                if (poolable.gameObject.activeInHierarchy) 
                    continue;

                poolDict[poolID].activeCount++;
                
                poolable.gameObject.SetActive(true);
                return (T) poolable;
            }

            poolDict[poolID].activeCount++;
            return (T) CreateAndAdd(poolDict[poolID], poolDict[poolID].ObjectsInPool, true);
        }

        public void ReturnToPool(Poolable poolable)
        {
            poolable.Pool.activeCount--;
            poolable.gameObject.SetActive(false);
        }
    }
}