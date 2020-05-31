using System;
using System.Collections.Generic;
using ao.wwisepooler.helpers.editor;
using UnityEngine;

namespace ao.wwisepooler
{
    [Serializable]
    public class Pool
    {
        public string Id;
        public Poolable Prefab;
        public int Size;
        
        [DisplayWithoutEdit] public int ActiveCount = 0;

        private List<Poolable> objectsInPool = new List<Poolable>();
        private Transform parent;

        public List<Poolable> ObjectsInPool => objectsInPool;
        public Transform Parent => parent;
        
        public void SetObjects(List<Poolable> objs)
        {
            objectsInPool = objs;
        }

        public void SetParent(Transform p)
        {
            parent = p;
        }
    }
}