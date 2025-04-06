using _Project.Scripts.Data;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using _Project.Scripts.Infrastructure.Services.SaveLoad;
using DebugToolsPlus;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.States
{
    public class LoadProgressState :
        IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(
            GameStateMachine stateMachine,
            ISaveLoadService saveLoadService,
            IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            D.Log("NEW PROGRESS","Create New Progress",DColor.MAGENTA);
            
            return new PlayerProgress(0);
        }
    }
}