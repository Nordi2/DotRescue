using System;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerMoveController : 
        IGameUpdateListener,
        IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IMovable _playerMovement;
        
        public PlayerMoveController(
            IInputService inputService,
            IMovable playerMovement)
        {
            _inputService = inputService;
            _playerMovement = playerMovement;

            _inputService.OnClickLeftMouseButton += ClickMouseButton;
        }

        void IGameUpdateListener.Update(float deltaTime) => 
            _playerMovement.Update(deltaTime);

        void IDisposable.Dispose() => 
            _inputService.OnClickLeftMouseButton -= ClickMouseButton;

        private void ClickMouseButton() => 
            _playerMovement.SwitchSides();
    }
}