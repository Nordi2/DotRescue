using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;

namespace _Project.Scripts.Gameplay.PauseText
{
    public class PauseTextPresenter :
        IGameFinishListener
    {
        private readonly IInputService _inputService;
        private readonly PauseTextView _pauseTextView;
        
        public PauseTextPresenter(
            PauseTextView pauseTextView,
            IInputService inputService)
        {
            _pauseTextView = pauseTextView;
            _inputService = inputService;
            
            _inputService.OnClickLeftMouseButton += DeactivateText;
        }

        void IGameFinishListener.FinishGame()
        {
            _inputService.OnClickLeftMouseButton -= DeactivateText;
        }

        private void DeactivateText()
        {
            _pauseTextView.StopAnimation();
            _inputService.OnClickLeftMouseButton -= DeactivateText;
        }
    }
}