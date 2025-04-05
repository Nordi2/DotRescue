﻿using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using UnityEngine;

namespace _Project.Scripts.Gameplay.PopupScoring
{
    public class PopupScoringPresenter : 
        IGameStartListener,
        IGameFinishListener
    {
        private PopupScoringView _view;
        private IGameOverEvent _gameOverEvent;
        private StorageScore _storageScore;
        
        private Vector2 _targetBodyPosition;
        private Vector2 _startShift;

        public PopupScoringPresenter(
            PopupScoringView view,
            IGameOverEvent gameOverEvent,
            StorageScore storageScore)
        {
            _view = view;
            _gameOverEvent = gameOverEvent;
            _storageScore = storageScore;
        }

        void IGameStartListener.StartGame()
        {
            _targetBodyPosition = _view.Body.anchoredPosition;
            _startShift = new Vector2(_targetBodyPosition.x, -Screen.height / 2);
            
            _gameOverEvent.OnGameOver += ShowPopup;
        }

        void IGameFinishListener.FinishGame() => 
            _gameOverEvent.OnGameOver -= ShowPopup;

        private void ShowPopup()
        {
             _view.ShowPopup(_targetBodyPosition, _startShift,(int)_storageScore.CurrentScore);   
        }
    }
}