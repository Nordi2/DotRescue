using System;
using _Project.Scripts.Infrastructure.Services.GameLoop;

namespace _Project.Scripts.Gameplay.Score
{
    public class StorageScore :
        IGameUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        public event Action<int> OnScoreChanged;

        private float _scoreSpeed;

        private int _oldScore;
        private bool _isPauseGame = true;

        public StorageScore(
            float scoreSpeed,
            float currentScore)
        {
            _scoreSpeed = scoreSpeed;
            CurrentScore = currentScore;

            _oldScore = (int)CurrentScore;
        }

        public float CurrentScore { get; private set; }

        void IGameStartListener.StartGame()
        {
            _isPauseGame = false;
        }

        void IGameFinishListener.FinishGame()
        {
            _isPauseGame = true;
        }

        void IGameUpdateListener.Update(float deltaTime)
        {
            if (_isPauseGame)
                return;
            
            CurrentScore += _scoreSpeed * deltaTime;
            CheckScore();
        }

        private void CheckScore()
        {
            if (!(CurrentScore >= _oldScore + 1))
                return;
            
            _oldScore = (int)CurrentScore;
            OnScoreChanged?.Invoke((int)CurrentScore);
        }
    }
}