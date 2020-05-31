using UnityEngine;
using Event = AK.Wwise.Event;

namespace ao.wwisepooler
{
    public class PoolEventPoster : EventPoster
    {
        private static readonly string poolName = AudioPool.Ambient.ToString();
        
        public override void Post(Event audioEvent, GameObject gameObject, string name)
        {
            var poolable = Pooler.Instance.RequestFromPool<AudioPoolable>(poolName);
            poolable.Post(audioEvent, gameObject, name);
        }
    }
}