using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Services;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IUIFactory : 
        IService
    {
        void CreateUIRoot();
        PauseTextView CreateInitialPauseText();
        PopupScoringView CreatePopupScoring(IGameOverEvent gameOverEvent, StorageScore storageScore);
    }
}