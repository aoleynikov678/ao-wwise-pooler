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
                var poolable = Pooler.Instance.RequestPoolable();
            }
        }

        public void SetInputKey(KeyCode keyCode)
        {
            inputKey = keyCode;
        }
    }
}