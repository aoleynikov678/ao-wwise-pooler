using UnityEngine;

namespace ao.wwisepooler
{
    public class AudioPoolable : Poolable
    {
        private GameObject targetObj;
        
        public void Post(AK.Wwise.Event audioEvent, GameObject target)
        {
            targetObj = target;
            audioEvent.Post(gameObject);
        }

        private void LateUpdate()
        {
            gameObject.transform.position = targetObj.transform.position;
            gameObject.transform.rotation = targetObj.transform.rotation;
        }
    }
}