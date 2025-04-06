using _Project.Scripts.Gameplay.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    [UsedImplicitly]
    public class PlayerMovement : 
        IMovable
    {
        private float _rotationSpeed;

        private readonly Transform _playerTransform;
        
        public PlayerMovement(
            float rotationSpeed,
            Transform playerTransform)
        {
            _rotationSpeed = rotationSpeed;
            _playerTransform = playerTransform;
        }

        public void Update(float deltaTime)
        {
            _playerTransform.Rotate(new Vector3(0, 0, _rotationSpeed * deltaTime));
        }

        public void SwitchSides() => 
            _rotationSpeed *= -1;

        public void SetRotationSpeed(float rotationSpeed)
        { }
    }
}