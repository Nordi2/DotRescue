using System;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerMoveController : 
        IGameUpdateListener,
        IGameStartListener,
        IGameFinishListener
    {
        private bool _isGamePaused = true;
        
        private readonly IInputService _inputService;
        private readonly IMovable _playerMovement;
        
        public PlayerMoveController(
            IInputService inputService,
            IMovable playerMovement)
        {
            _inputService = inputService;
            _playerMovement = playerMovement;
        }

        void IGameUpdateListener.Update(float deltaTime)
        {
            if(_isGamePaused)
                return;
            
            _playerMovement.Update(deltaTime);
        }

        void IGameStartListener.StartGame()
        {
            _isGamePaused = false;
            _inputService.OnClickLeftMouseButton += ClickMouseButton;
        }

        void IGameFinishListener.FinishGame()
        {
            _inputService.OnClickLeftMouseButton -= ClickMouseButton;
        }

        private void ClickMouseButton() => 
            _playerMovement.SwitchSides();
    }
}