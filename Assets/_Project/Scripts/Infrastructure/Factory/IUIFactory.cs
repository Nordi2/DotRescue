using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IUIFactory : 
        IService
    {
        void CreateUIRoot();
        PauseTextView CreateInitialPauseText(IInputService inputService,IGameLoopService gameLoopService);
        PopupScoringView CreatePopupScoring(IGameOverEvent gameOverEvent, StorageScore storageScore,IGameLoopService gameLoopService);
        void CreateMainMenu();
    }
}