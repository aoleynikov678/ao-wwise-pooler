using System.Collections.Generic;
using UnityEngine;

namespace ao.wwisepooler
{
    public class Pooler : MonoBehaviour
    {
        [SerializeField] private Poolable prefab;
        [SerializeField] private int count;
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

            set { instance = value; }
        }

        private void Awake()
        {
            Instance = this;

            pool = GeneratePool(count);
        }

        private List<Poolable> GeneratePool(int poolSize)
        {
            var p = new List<Poolable>();
            
            for (int i = 0; i < poolSize; i++)
            {
                var poolable = Instantiate(prefab, transform, true);
                poolable.gameObject.SetActive(false);
                
                p.Add(poolable);
            }

            return p;
        }
    }

    public abstract class Poolable : MonoBehaviour
    {
    }
}