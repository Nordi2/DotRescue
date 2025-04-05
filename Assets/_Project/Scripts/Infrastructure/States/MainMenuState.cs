using _Project.Scripts.Infrastructure.Factory;

namespace _Project.Scripts.Infrastructure.States
{
    public class MainMenuState :
        IState
    {
        private const string Gameplay = "Gameplay";
        private const string MainMenu = "MainMenu";
        
        private LoadingCurtain _curtain;
        private SceneLoader _sceneLoader;
        private IUIFactory _uiFactory;
        
        public MainMenuState(
            LoadingCurtain curtain,
            SceneLoader sceneLoader,
            IUIFactory uiFactory)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _curtain.Hide();
            _sceneLoader.Load(MainMenu,OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateMainMenu();
        }
    }
}