using System;
using UnityEngine;
using UnityEngine.Profiling;
using Random = UnityEngine.Random;

namespace ao.wwisepooler
{
    public class SoundBox : MonoBehaviour
    {
        [SerializeField] private AK.Wwise.Event audioEvent;

        [SerializeField, Range(0f, 1f)] private float probability = 0.8f;
        [SerializeField] private float minDelay = 1f;
        [SerializeField] private float maxDelay = 2f;

        [SerializeField] private Animator animator;
        [SerializeField] private AudioPoolType poolType;
        
        private EventPoster eventPoster;
        private float prevEventTime = 0;
        private float delay;
        
        private string cachedObjName;
        private string poolName;
        private static readonly int TweenParam = Animator.StringToHash("Tween");
        
        private void Awake()
        {
            delay = Random.Range(minDelay, maxDelay);
            prevEventTime = -delay;
            cachedObjName = gameObject.name;

            poolName = poolType.ToString();
            
            eventPoster = new PoolEventPoster();
        }

        public void Update()
        {
            if (Random.value > (1 - probability) && Time.time - prevEventTime > delay)
            {
                prevEventTime = Time.time;
                
                Profiler.BeginSample("EventPoster");
                eventPoster.Post(audioEvent, gameObject, cachedObjName, poolName);
                Profiler.EndSample();
                
                animator.SetTrigger(TweenParam);
            }
        }
    }
}