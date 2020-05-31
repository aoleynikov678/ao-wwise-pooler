using System;
using System.Collections.Generic;

namespace ao.wwisepooler
{
    [Serializable]
    public class Pool
    {
        public string id;
        public Poolable prefab;
        public int size;
        public int activeCount;

        private List<Poolable> objectsInPool = new List<Poolable>();
        public List<Poolable> ObjectsInPool => objectsInPool;

        public void SetObjects(List<Poolable> objs)
        {
            objectsInPool = objs;
        }
    }
}