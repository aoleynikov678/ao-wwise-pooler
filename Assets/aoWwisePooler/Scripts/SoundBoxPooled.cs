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
                StartCoroutine(Hide(poolable));
            }
        }

        public void SetInputKey(KeyCode keyCode)
        {
            inputKey = keyCode;
        }

        private IEnumerator Hide(Poolable poolable)
        {
            yield return new WaitForSeconds(0.5f);
            Pooler.Instance.ReturnToPool(poolable);
        }
    }
}