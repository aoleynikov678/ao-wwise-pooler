using System.Collections;
using UnityEngine;

namespace ao.wwisepooler
{
    public class SoundBoxPooled : MonoBehaviour, IHaveInput
    {
        [SerializeField] private AK.Wwise.Event audioEvent;
        [SerializeField] private KeyCode inputKey;
        
        [SerializeField, Range(0f, 1f)] private float probability = 0.8f;
        [SerializeField] private float minDelay = 1f;
        [SerializeField] private float maxDelay = 2f;

        private float prevEventTime = 0;
        private float delay;

        private void Awake()
        {
            delay = Random.Range(minDelay, maxDelay);
            prevEventTime = delay;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(inputKey))
            {
                var poolable = (AudioPoolable) Pooler.Instance.RequestFromPool();
                poolable.Post(audioEvent, gameObject);
            }
            
            if (Random.value > (1 - probability) && Time.time - prevEventTime > delay)
            {
                prevEventTime = Time.time;
                
                var poolable = (AudioPoolable) Pooler.Instance.RequestFromPool();
                poolable.Post(audioEvent, gameObject);
            }
        }

        public void SetInputKey(KeyCode keyCode)
        {
            inputKey = keyCode;
        }
    }
}