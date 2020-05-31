using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ao.wwisepooler
{
    public class SoundBox : MonoBehaviour
    {
        [SerializeField] private EventPostType eventPostType;
        [SerializeField] private AK.Wwise.Event audioEvent;

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
            if (Random.value > (1 - probability) && Time.time - prevEventTime > delay)
            {
                prevEventTime = Time.time;
                
                eventPoster.Post(audioEvent, gameObject);
            }
        }
    }
}