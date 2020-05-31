using UnityEngine;

namespace ao.wwisepooler
{
    public abstract class Poolable : MonoBehaviour
    {
        protected Pooler pooler;
        protected Pool pool;
        public abstract void SetPooler(Pooler pooler, Pool pool);
        public Pool Pool => pool;
    }
}