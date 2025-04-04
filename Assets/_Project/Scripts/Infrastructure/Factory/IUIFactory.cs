using _Project.Scripts.Gameplay.UI;
using _Project.Scripts.Infrastructure.Services;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IUIFactory : 
        IService
    {
        void CreateUIRoot();
        PauseTextView CreateInitialPauseText();
    }
}