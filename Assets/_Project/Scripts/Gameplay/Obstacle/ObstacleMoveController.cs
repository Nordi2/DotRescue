using System;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.StaticData;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Obstacle
{
    public class ObstacleMoveController :
        IGameUpdateListener,
        IDisposable
    {
        private readonly IMovable _obstacleMovement;
        private readonly ObstacleStatsTimeSwitcher _obstacleStatsTimeSwitcher;
        private readonly ObstacleConfig _config;
        
        private float _rotateTime;
        private float _currentRotationSpeed;
        
        public ObstacleMoveController(
            IMovable obstacleMovement,
            ObstacleStatsTimeSwitcher obstacleStatsTimeSwitcher,
            ObstacleConfig config)
        {
            _obstacleMovement = obstacleMovement;
            _obstacleStatsTimeSwitcher = obstacleStatsTimeSwitcher;
            _config = config;

            _obstacleStatsTimeSwitcher.OnCompleteTimer += CompleteTimer;
            RandomizeStatsObstacle();
        }

        void IGameUpdateListener.Update(float deltaTime)
        {
            _obstacleStatsTimeSwitcher.Update(deltaTime);
            _obstacleMovement.Update(deltaTime);
        }

        void IDisposable.Dispose() => 
            _obstacleStatsTimeSwitcher.OnCompleteTimer -= CompleteTimer;

        private void RandomizeStatsObstacle()
        {
            _currentRotationSpeed = _config.GetRandomRotationSpeed();
            _rotateTime = _config.GetRandomRotateTime();
            
            _currentRotationSpeed *= Random.Range(0, 2) == 0 
                ? 1f 
                : -1f;
            
            _obstacleStatsTimeSwitcher.SetRotateTime(_rotateTime);
            _obstacleMovement.SetRotationSpeed(_currentRotationSpeed);
        }

        private void CompleteTimer() => 
            RandomizeStatsObstacle();
    }
}