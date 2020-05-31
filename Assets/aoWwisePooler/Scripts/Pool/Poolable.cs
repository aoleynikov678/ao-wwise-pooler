using UnityEngine;

namespace ao.wwisepooler
{
    public abstract class Poolable : MonoBehaviour
    {
        protected Pooler pooler;
        public abstract void SetPooler(Pooler p);
    }
}