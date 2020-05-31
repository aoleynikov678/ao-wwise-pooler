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
        
        public override void SetPooler(Pooler pooler, Pool pool)
        {
            this.pooler = pooler;
            this.pool = pool;
        }

        public void Post(AK.Wwise.Event audioEvent, GameObject target, string targetName)
        {
            AkSoundEngine.RegisterGameObj(gameObject, targetName);
            targetObj = target;
            audioEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, OnCallback);
        }

        private void OnCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            AkSoundEngine.UnregisterGameObj(gameObject);
            pooler.ReturnToPool(this);
        }

        private void LateUpdate()
        {
            poolableTransform.position = targetObj.transform.position;
            poolableTransform.rotation = targetObj.transform.rotation;
        }
    }
}