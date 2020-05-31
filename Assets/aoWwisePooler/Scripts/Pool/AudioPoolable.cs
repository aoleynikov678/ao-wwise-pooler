using UnityEngine;

namespace ao.wwisepooler
{
    public class AudioPoolable : Poolable
    {
        private GameObject targetObj;
        private Transform poolableTransform;

        private void Awake()
        {
            poolableTransform = transform;
        }
        
        public override void SetPooler(Pooler p)
        {
            pooler = p;
        }

        public void Post(AK.Wwise.Event audioEvent, GameObject target)
        {
            targetObj = target;
            audioEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, OnCallback);
        }

        private void OnCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            pooler.ReturnToPool(this);
        }

        private void LateUpdate()
        {
            poolableTransform.position = targetObj.transform.position;
            poolableTransform.rotation = targetObj.transform.rotation;
        }
    }
}