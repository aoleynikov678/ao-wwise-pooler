using UnityEngine;

namespace ao.wwisepooler
{
    public class OscillatorMover : MonoBehaviour
    {
        [SerializeField] private float height = 2;
        [SerializeField] private float speed = 10;
        
        private float timer = 0;

        private void Update()
        {
            timer += Time.deltaTime * speed;

            var y = Mathf.Sin(timer) * height;
            
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
}