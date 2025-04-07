using _Project.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Obstacle
{
    public class ObstacleMovement : 
        IMovable
    {
        private float _rotationSpeed;

        private readonly Transform _obstacleTransform;
        
        public ObstacleMovement(Transform obstacleTransform)
        {
            _obstacleTransform = obstacleTransform;
        }

        public void SetRotationSpeed(float rotationSpeed) => 
            _rotationSpeed = rotationSpeed;

        public void Update(float deltaTime) =>
            _obstacleTransform.Rotate(new Vector3(0,0, _rotationSpeed * deltaTime));

        public void SwitchSides()
        { }
    }
}