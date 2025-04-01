using UnityEngine;

namespace _Project_Test.Scripts
{
    public class Player : MonoBehaviour
    {
        public float _rotateSpeed;
        
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _rotateSpeed *= -1f;
            }    
        }
        
        private void FixedUpdate()
        {
            transform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                Destroy(gameObject);
        }
    }
}
