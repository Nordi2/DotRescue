using System;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Infrastructure.Services.Input;

namespace _Project.Scripts.Infrastructure.Services.GameLoop
{
    public class GameLoopController :
        IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IGameLoopService _gameLoopService;
        private readonly IGameOverEvent _gameOver;
        
        public GameLoopController(
            IInputService inputService,
            IGameLoopService gameLoopService,
            IGameOverEvent gameOver)
        {
            _inputService = inputService;
            _gameLoopService = gameLoopService;
            _gameOver = gameOver;

            _gameOver.OnGameOver += FinishGame;
            _inputService.OnClickLeftMouseButton += StartGame;
        }

        void IDisposable.Dispose() => 
            _gameOver.OnGameOver -= FinishGame;

        private void FinishGame() => 
            _gameLoopService.OnFinishGame();

        private void StartGame()
        {
            _inputService.OnClickLeftMouseButton -= StartGame;
            _gameLoopService.OnStartGame();
        }
    }
}