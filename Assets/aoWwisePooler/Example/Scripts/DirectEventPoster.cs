using UnityEngine;
using Event = AK.Wwise.Event;

namespace ao.wwisepooler
{
    public class DirectEventPoster : EventPoster
    {
        public override void Post(Event audioEvent, GameObject gameObject, string name)
        {
            audioEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, OnCallback);
        }
        
        private void OnCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
        }
    }
}