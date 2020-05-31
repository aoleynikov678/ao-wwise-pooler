using UnityEngine;

namespace ao.wwisepooler
{
    public class PoolEventPoster : EventPoster
    {
        public override void Post(AK.Wwise.Event audioEvent, GameObject gameObject, string name, string poolName)
        {
            var poolable = Pooler.Instance.RequestFromPool<AudioPoolable>(poolName);
            poolable.Post(audioEvent, gameObject, name);
        }
    }
}