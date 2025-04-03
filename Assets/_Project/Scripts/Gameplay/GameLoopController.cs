using System;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;

namespace _Project.Scripts.Gameplay
{
    public class GameLoopController :
        IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IGameLoopService _gameLoopService;
        private readonly PlayerFacade _player;
        
        public GameLoopController(
            IInputService inputService,
            IGameLoopService gameLoopService,
            PlayerFacade player)
        {
            _inputService = inputService;
            _gameLoopService = gameLoopService;
            _player = player;
            
            Subscribe();
        }

        void IDisposable.Dispose() => 
            _player.OnDeath -= FinishGame;

        private void FinishGame() => 
            _gameLoopService.OnFinishGame();

        private void StartGame()
        {
            _inputService.OnClickLeftMouseButton -= StartGame;
            _gameLoopService.OnStartGame();
        }

        private void Subscribe()
        {
            _player.OnDeath += FinishGame;
            _inputService.OnClickLeftMouseButton += StartGame;
        }
    }
}