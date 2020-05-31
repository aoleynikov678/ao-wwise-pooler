using System.Collections.Generic;
using UnityEngine;

namespace ao.wwisepooler
{
    public class Pooler : MonoBehaviour
    {
        [SerializeField] private Poolable prefab;
        [SerializeField] private int size;
        [SerializeField] private int activeCount = 0;
        
        private List<Poolable> pool = new List<Poolable>();
        
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
            Instance = this;

            pool = GeneratePool(size);
        }

        private List<Poolable> GeneratePool(int poolSize)
        {
            var p = new List<Poolable>();
            
            for (int i = 0; i < poolSize; i++)
            {
                CreateAndAdd(p, false);
            }

            return p;
        }

        private Poolable CreateAndAdd(List<Poolable> p, bool setActive)
        {
            var poolable = Instantiate(prefab, transform, true);
            poolable.gameObject.SetActive(setActive);
            poolable.SetPooler(this);
                
            p.Add(poolable);

            return poolable;
        }

        public Poolable RequestFromPool()
        {
            foreach (var poolable in pool)
            {
                if (!poolable.gameObject.activeInHierarchy)
                {
                    poolable.gameObject.SetActive(true);
                    activeCount++;
                    return poolable;
                }
            }

            activeCount++;
            return CreateAndAdd(pool, true);
        }

        public void ReturnToPool(Poolable poolable)
        {
            activeCount--;
            poolable.gameObject.SetActive(false);
        }
    }
}