using _Project.Scripts.Infrastructure.Services.SaveLoad;

namespace _Project.Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        
        public GameLoopState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Exit()
        {
            _saveLoadService.SaveProgress();
        }

        public void Enter()
        {
        }
    }
}