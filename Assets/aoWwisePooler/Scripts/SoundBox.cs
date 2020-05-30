using UnityEngine;

namespace ao.wwisepooler
{
    public class SoundBox : MonoBehaviour, IHaveInput
    {
        [SerializeField] private AK.Wwise.Event audioEvent;
        [SerializeField] private KeyCode inputKey;

        private void Update()
        {
            if (Input.GetKeyDown(inputKey))
            {
                audioEvent.Post(gameObject);
            }
        }

        public void SetInputKey(KeyCode keyCode)
        {
            inputKey = keyCode;
        }
    }

    public interface IHaveInput
    {
        void SetInputKey(KeyCode keyCode);
    }
}