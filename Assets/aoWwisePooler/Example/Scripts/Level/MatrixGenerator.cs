using UnityEngine;

namespace ao.wwisepooler
{
    public class MatrixGenerator : MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private Transform pivot;
        [SerializeField] private float radius;
        [SerializeField] private float height;
        
        [SerializeField] private GameObject cellPrefab;

        private void Awake()
        {
            var angleStepY = 180f / (count - 1);
            var curAngleY = -90f;

            for (var i = 0; i < count; i++)
            {
                GenerateCell(pivot.position, curAngleY, i);
                curAngleY += angleStepY;
            }
        }

        private void GenerateCell(Vector3 center, float angle, int counter)
        {
            var rad = Mathf.PI * angle / 180.0f;
            var x = center.x + radius * Mathf.Sin(rad);
            var z = center.z + radius * Mathf.Cos(rad);
            var cell = Instantiate(cellPrefab, transform, true);
            cell.transform.position = new Vector3(x, height, z);
        }
    }
}