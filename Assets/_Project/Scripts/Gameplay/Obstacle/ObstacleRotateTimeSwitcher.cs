using System;
using JetBrains.Annotations;

namespace _Project.Scripts.Gameplay.Obstacle
{
    [UsedImplicitly]
    public class ObstacleStatsTimeSwitcher
    {
        public event Action OnCompleteTimer;

        private float _rotateTime;
        private float _currentRotateTime;

        public void SetRotateTime(float rotateTime) => 
            _rotateTime = rotateTime;

        public void Update(float deltaTime)
        {
            _currentRotateTime += deltaTime;

            if (!(_currentRotateTime >= _rotateTime))
                return;
            
            _currentRotateTime = 0;
            OnCompleteTimer?.Invoke();
        }
    }
}