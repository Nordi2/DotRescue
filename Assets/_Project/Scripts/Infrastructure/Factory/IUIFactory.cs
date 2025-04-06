using System.Collections.Generic;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IUIFactory : 
        IService
    {
        List<ILoadProgress> LoadProgresses { get; }
        void CleanUp();
        void CreateUIRoot();
        PauseTextView CreateInitialPauseText(IInputService inputService,IGameLoopService gameLoopService);
        PopupScoringView CreatePopupScoring(IGameOverEvent gameOverEvent, StorageScore storageScore,IGameLoopService gameLoopService);
        void CreateMainMenu();
    }
}