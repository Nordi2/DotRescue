using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.States;
using UnityEngine;

namespace _Project.Scripts.Gameplay.PopupScoring
{
    public class PopupScoringPresenter : 
        IGameStartListener,
        IGameFinishListener
    {
        private Vector2 _targetBodyPosition;
        private Vector2 _startShift;
        
        private readonly PopupScoringView _view;
        private readonly IGameOverEvent _gameOverEvent;
        private readonly StorageScore _storageScore;
        private readonly IGameStateMachine _stateMachine;

        public PopupScoringPresenter(
            PopupScoringView view,
            IGameOverEvent gameOverEvent,
            StorageScore storageScore,
            IGameStateMachine stateMachine)
        {
            _view = view;
            _gameOverEvent = gameOverEvent;
            _storageScore = storageScore;
            _stateMachine = stateMachine;
        }

        void IGameStartListener.StartGame()
        {
            _targetBodyPosition = _view.Body.anchoredPosition;
            _startShift = new Vector2(_targetBodyPosition.x, -Screen.height / 2);
            
            _view.ClickExitButton(GoToMainMenuScene);
            _gameOverEvent.OnGameOver += ShowPopup;
        }

        void IGameFinishListener.FinishGame() => 
            _gameOverEvent.OnGameOver -= ShowPopup;

        private void GoToMainMenuScene() => 
            _stateMachine.Enter<MainMenuState>();

        private void ShowPopup()
        {
             _view.ShowPopup(_targetBodyPosition, _startShift,(int)_storageScore.CurrentScore);   
        }
    }
}