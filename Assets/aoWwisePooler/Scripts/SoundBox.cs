using System;
using UnityEngine;
using Event = AK.Wwise.Event;
using Random = UnityEngine.Random;

namespace ao.wwisepooler
{
    public enum EventPostType
    {
        Direct,
        Pool
    }
    
    
    public class SoundBox : MonoBehaviour, IHaveInput
    {
        [SerializeField] private EventPostType eventPostType;
        
        [SerializeField] private AK.Wwise.Event audioEvent;
        [SerializeField] private KeyCode inputKey;

        [SerializeField, Range(0f, 1f)] private float probability = 0.8f;
        [SerializeField] private float minDelay = 1f;
        [SerializeField] private float maxDelay = 2f;

        private float prevEventTime = 0;
        private float delay;

        private EventPoster eventPoster;

        private void Awake()
        {
            delay = Random.Range(minDelay, maxDelay);
            prevEventTime = delay;

            switch (eventPostType)
            {
                case EventPostType.Direct:
                    eventPoster = new DirectEventPoster();
                    break;
                case EventPostType.Pool:
                    eventPoster = new PoolEventPoster();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(inputKey))
            {
                eventPoster.Post(audioEvent, gameObject);
            }

            if (Random.value > (1 - probability) && Time.time - prevEventTime > delay)
            {
                prevEventTime = Time.time;
                
                eventPoster.Post(audioEvent, gameObject);
            }
        }

        public void SetInputKey(KeyCode keyCode)
        {
            inputKey = keyCode;
        }
    }

    public abstract class EventPoster
    {
        public abstract void Post(AK.Wwise.Event audioEvent, GameObject gameObject);
    }

    public class DirectEventPoster : EventPoster
    {
        public override void Post(Event audioEvent, GameObject gameObject)
        {
            audioEvent.Post(gameObject);
        }
    }

    public class PoolEventPoster : EventPoster
    {
        public override void Post(Event audioEvent, GameObject gameObject)
        {
            var poolable = (AudioPoolable) Pooler.Instance.RequestFromPool();
            poolable.Post(audioEvent, gameObject);
        }
    }

    public interface IHaveInput
    {
        void SetInputKey(KeyCode keyCode);
    }
}