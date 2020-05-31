using System.Collections;
using UnityEngine;

namespace ao.wwisepooler
{
    public class SoundBoxPooled : MonoBehaviour, IHaveInput
    {
        [SerializeField] private AK.Wwise.Event audioEvent;
        [SerializeField] private KeyCode inputKey;

        private void Update()
        {
            if (Input.GetKeyDown(inputKey))
            {
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