using UnityEngine;

namespace _Project_Test.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _minRotateSpeed;
        [SerializeField] private float _maxRotateSpeed;

        [SerializeField] private float _maxRotateTime;
        [SerializeField] private float _minRotateTime;
        
        public float rotateTime;
        public float currentRotateSpeed;
        public float currentRotateTime;

        private void Awake()
        {
            currentRotateTime = 0f;
            currentRotateSpeed = _minRotateSpeed + (_maxRotateSpeed - _minRotateSpeed) * 0.1f * Random.Range(0,11);
            rotateTime = _minRotateTime + (_maxRotateTime - _minRotateTime) * 0.1f * Random.Range(0,11);
            currentRotateSpeed *= Random.Range(0, 2) == 0 ? 1f : -1f;
        }

        private void Update()
        {
            currentRotateTime += Time.deltaTime;

            if(currentRotateTime > rotateTime)
            {
                currentRotateTime = 0f;
                currentRotateSpeed = _minRotateSpeed + (_maxRotateSpeed - _minRotateSpeed) * 0.1f * Random.Range(0, 11);
                rotateTime = _minRotateTime + (_maxRotateTime - _minRotateTime) * 0.1f * Random.Range(0, 11);
                currentRotateSpeed *= Random.Range(0, 2) == 0 ? 1f : -1f;
            }
        }

        private void FixedUpdate()
        {
            transform.Rotate(0, 0, currentRotateSpeed * Time.fixedDeltaTime);
        }
    }
}
