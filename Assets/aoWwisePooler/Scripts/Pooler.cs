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

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region Public API
        
        /// <summary>
        /// Initialize all the pools from the list
        /// </summary>
        public void Initialize()
        {
            Instance = this;

            foreach (var pool in pools)
            {
                var objs = GeneratePool(pool);

                if (PoolExists(pool.Id)) 
                    continue;
                
                pool.SetObjects(objs);
                poolDict.Add(pool.Id, pool);
            }
        }
        
        /// <summary>
        /// Get the poolable object of the type from the pool
        /// </summary>
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

                poolDict[poolID].ActiveCount++;
                
                poolable.gameObject.SetActive(true);
                return (T) poolable;
            }

            poolDict[poolID].ActiveCount++;
            return (T) CreateAndAdd(poolDict[poolID], poolDict[poolID].ObjectsInPool, true);
        }

        /// <summary>
        /// Return pbject to the pool
        /// </summary>
        public void ReturnToPool(Poolable poolable)
        {
            poolable.Pool.ActiveCount--;
            poolable.gameObject.SetActive(false);
        }
        
        #endregion

        #region Pool Management
        
        private List<Poolable> GeneratePool(Pool poolDescriptor)
        {
            if (poolDescriptor.Size <= 0)
                return null;
            
            var go = new GameObject($"{poolDescriptor.Id}");
            go.transform.parent = transform;
            poolDescriptor.SetParent(go.transform);
            
            var p = new List<Poolable>();
            
            for (int i = 0; i < poolDescriptor.Size; i++)
            {
                CreateAndAdd(poolDescriptor, p, false);
            }

            return p;
        }

        private Poolable CreateAndAdd(Pool poolDescriptor, List<Poolable> p, bool setActive)
        {
            var poolable = Instantiate(poolDescriptor.Prefab, poolDescriptor.Parent, true);
            poolable.gameObject.SetActive(setActive);
            poolable.SetPooler(this, poolDescriptor);
                
            p.Add(poolable);

            return poolable;
        }
        
        private bool PoolExists(string poolID)
        {
            return poolDict.ContainsKey(poolID);
        }
        
        #endregion
    }
}