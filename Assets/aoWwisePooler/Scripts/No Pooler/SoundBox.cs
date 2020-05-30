using UnityEngine;

namespace ao.wwisepooler
{
    public class SoundBox : MonoBehaviour
    {
        [SerializeField] private AK.Wwise.Event audioEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioEvent.Post(gameObject);
            }
        }
    }
}