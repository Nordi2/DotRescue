using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Obstacle
{
    public class ObstacleMoveController :
        IGameUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        private readonly IMovable _obstacleMovement;
        private readonly ObstacleStatsTimeSwitcher _obstacleStatsTimeSwitcher;
        private readonly ObstacleConfig _config;

        private float _rotateTime;
        private float _currentRotationSpeed;
        private bool _isStartGame = true;
        
        public ObstacleMoveController(
            IMovable obstacleMovement,
            ObstacleStatsTimeSwitcher obstacleStatsTimeSwitcher,
            ObstacleConfig config)
        {
            _obstacleMovement = obstacleMovement;
            _obstacleStatsTimeSwitcher = obstacleStatsTimeSwitcher;
            _config = config;
        }

        void IGameUpdateListener.Update(float deltaTime)
        {
            if(_isStartGame)
                return;
            
            _obstacleStatsTimeSwitcher.Update(deltaTime);
            _obstacleMovement.Update(deltaTime);
        }

        void IGameStartListener.StartGame()
        {
            _isStartGame = false;
            
            _obstacleStatsTimeSwitcher.OnCompleteTimer += CompleteTimer;
            RandomizeStatsObstacle();
        }

        void IGameFinishListener.FinishGame()
        {
            _isStartGame = true;
            
            _obstacleStatsTimeSwitcher.OnCompleteTimer -= CompleteTimer;
        }

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