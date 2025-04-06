using _Project.Scripts.Infrastructure;
using _Project.Scripts.Infrastructure.States;

namespace _Project.Scripts.MainMenu
{
    public class MainMenuPresenter
    {
        private const string Gameplay = "Gameplay";
        
        private readonly MainMenuView _view;
        private readonly IGameStateMachine _gameStateMachine;
        
        public MainMenuPresenter(
            MainMenuView view,
            IGameStateMachine gameStateMachine)
        {
            _view = view;
            _gameStateMachine = gameStateMachine;

            _view.ClickStartGameButton(GoToGameplayScene);
        }

        private void GoToGameplayScene()
        {
            _gameStateMachine.Enter<LoadLevelState,string>(Gameplay);
        }
    }
}