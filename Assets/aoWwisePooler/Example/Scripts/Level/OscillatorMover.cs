using UnityEngine;

namespace ao.wwisepooler
{
    public class OscillatorMover : MonoBehaviour
    {
        [SerializeField] private float height = 2;
        [SerializeField] private float speed = 10;
        
        private float timer = 0;
        private float randomPhase = 0;

        private void Awake()
        {
            randomPhase = Random.value * 10;
        }

        private void Update()
        {
            timer += Time.deltaTime * speed;

            var z = Mathf.Sin(randomPhase + timer) * height;
            
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
}