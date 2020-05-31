using UnityEngine;

namespace ao.wwisepooler
{
    public class OscillatorMover : MonoBehaviour
    {
        [SerializeField] private float range = 2;
        [SerializeField] private float speed = 10;
        
        private float timer = 0;
        private float randomPhase = 0;
        private Transform cachedTransform;

        private void Awake()
        {
            randomPhase = Random.value * 10;
            cachedTransform = transform;
        }

        private void Update()
        {
            timer += Time.deltaTime * speed;

            var z = Mathf.Sin(randomPhase + timer) * range;
            
            cachedTransform.position = new Vector3(transform.position.x, cachedTransform.position.y, z);
        }
    }
}