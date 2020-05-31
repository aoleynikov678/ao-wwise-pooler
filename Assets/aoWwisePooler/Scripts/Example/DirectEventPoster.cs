using UnityEngine;
using Event = AK.Wwise.Event;

namespace ao.wwisepooler
{
    public class DirectEventPoster : EventPoster
    {
        public override void Post(Event audioEvent, GameObject gameObject)
        {
            audioEvent.Post(gameObject);
        }
    }
}