using System;
using _Project.Scripts.Data;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Score
{
    public class StorageScore :
        IGameUpdateListener,
        IGameStartListener,
        IGameFinishListener,
        ISavedProgress
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

        void ISavedProgress.UpdateProgress(PlayerProgress progress)
        {
            if (_oldScore >= progress.ScoreData.Score)
                progress.ScoreData.Score = _oldScore;
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