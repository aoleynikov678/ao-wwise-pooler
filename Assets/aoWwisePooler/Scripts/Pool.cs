using System;
using System.Collections.Generic;
using ao.wwisepooler.helpers.editor;

namespace ao.wwisepooler
{
    [Serializable]
    public class Pool
    {
        public string id;
        public Poolable prefab;
        public int size;
        
        [DisplayWithoutEdit] public int activeCount = 0;

        private List<Poolable> objectsInPool = new List<Poolable>();
        public List<Poolable> ObjectsInPool => objectsInPool;

        public void SetObjects(List<Poolable> objs)
        {
            objectsInPool = objs;
        }
    }
}