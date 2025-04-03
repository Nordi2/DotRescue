using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.StaticData;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Obstacle
{
    public class ObstacleMoveController :
        IGameUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        private float _rotateTime;
        private float _currentRotationSpeed;
        private bool _isPauseGame = true;
        
        private readonly IMovable _obstacleMovement;
        private readonly ObstacleStatsTimeSwitcher _obstacleStatsTimeSwitcher;
        private readonly ObstacleConfig _config;
        
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
            if(_isPauseGame)
                return;
            
            _obstacleStatsTimeSwitcher.Update(deltaTime);
            _obstacleMovement.Update(deltaTime);
        }

        void IGameStartListener.StartGame()
        {
            _isPauseGame = false;
            
            _obstacleStatsTimeSwitcher.OnCompleteTimer += CompleteTimer;
            RandomizeStatsObstacle();
        }

        void IGameFinishListener.FinishGame()
        {
            _isPauseGame = true;
            
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