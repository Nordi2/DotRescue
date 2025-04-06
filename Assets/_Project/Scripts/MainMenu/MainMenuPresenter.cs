using _Project.Scripts.Data;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using _Project.Scripts.Infrastructure.States;

namespace _Project.Scripts.MainMenu
{
    public class MainMenuPresenter : 
        ILoadProgress
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

        private void GoToGameplayScene() => 
            _gameStateMachine.Enter<LoadLevelState,string>(Gameplay);

        public void LoadProgress(PlayerProgress progress) => 
            _view.UpdateTextScore($"BEST: {progress.ScoreData.Score.ToString()}");
    }
}