using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;

namespace _Project.Scripts.Infrastructure.States
{
    public class MainMenuState :
        IState
    {
        private const string MainMenu = "MainMenu";

        private LoadingCurtain _curtain;
        private SceneLoader _sceneLoader;
        private IUIFactory _uiFactory;
        private IPersistentProgressService _progressService;

        public MainMenuState(
            LoadingCurtain curtain,
            SceneLoader sceneLoader,
            IUIFactory uiFactory,
            IPersistentProgressService progressService)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _progressService = progressService;
        }

        public void Enter()
        {
            _curtain.Hide();
            _uiFactory.CleanUp();
            _sceneLoader.Load(MainMenu, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateMainMenu();

            foreach (ILoadProgress loadProgress in _uiFactory.LoadProgresses)
                loadProgress.LoadProgress(_progressService.Progress);
        }
    }
}