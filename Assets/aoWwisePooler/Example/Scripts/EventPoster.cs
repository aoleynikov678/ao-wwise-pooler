using UnityEngine;

namespace ao.wwisepooler
{
    public abstract class EventPoster
    {
        public abstract void Post(AK.Wwise.Event audioEvent, GameObject gameObject, string name);
    }
}