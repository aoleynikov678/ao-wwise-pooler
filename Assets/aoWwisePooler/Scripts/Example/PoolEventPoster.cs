using UnityEngine;
using Event = AK.Wwise.Event;

namespace ao.wwisepooler
{
    public class PoolEventPoster : EventPoster
    {
        public override void Post(Event audioEvent, GameObject gameObject)
        {
            var poolable = (AudioPoolable) Pooler.Instance.RequestFromPool();
            poolable.Post(audioEvent, gameObject);
        }
    }
}