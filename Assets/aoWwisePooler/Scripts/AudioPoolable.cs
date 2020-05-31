using UnityEngine;

namespace ao.wwisepooler
{
    public class AudioPoolable : Poolable
    {
        private GameObject targetObj;
        private Transform poolableTransform;

        private void Awake()
        {
            // cache this transform
            poolableTransform = transform;
        }
        
        /// <summary>
        /// Set pooler/pool data
        /// </summary>
        public override void SetPooler(Pooler pooler, Pool pool)
        {
            this.pooler = pooler;
            this.pool = pool;
        }

        /// <summary>
        /// Post event by using this poolable object
        /// </summary>
        public void Post(AK.Wwise.Event audioEvent, GameObject target, string targetName)
        {
            AkSoundEngine.RegisterGameObj(gameObject, targetName);
            targetObj = target;
            audioEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, OnCallback);
        }

        /// <summary>
        /// Return poolable to the pool when the event is finished
        /// </summary>
        private void OnCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            AkSoundEngine.UnregisterGameObj(gameObject);
            pooler.ReturnToPool(this);
        }

        /// <summary>
        /// Follow audio event trigger object to pass correct position/rotation data to wwise
        /// </summary>
        private void LateUpdate()
        {
            poolableTransform.position = targetObj.transform.position;
            poolableTransform.rotation = targetObj.transform.rotation;
        }
    }
}