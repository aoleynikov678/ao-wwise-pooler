using UnityEngine;

namespace ao.wwisepooler
{
    public class PoolEventPoster : EventPoster
    {
        private static readonly string poolName = AudioPoolType.Ambient.ToString();
        
        public override void Post(AK.Wwise.Event audioEvent, GameObject gameObject, string name)
        {
            var poolable = Pooler.Instance.RequestFromPool<AudioPoolable>(poolName);
            poolable.Post(audioEvent, gameObject, name);
        }
    }
}