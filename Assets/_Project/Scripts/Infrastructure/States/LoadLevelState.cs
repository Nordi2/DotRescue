using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.UI;
using _Project.Scripts.Infrastructure.Factory;
using DG.Tweening;

namespace _Project.Scripts.Infrastructure.States
{
    public class LoadLevelState :
        IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            DOTween.Init();
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            PlayerFacade playerFacade = _gameFactory.CreatePlayer();
            _gameFactory.CreatePlayArea();
            _gameFactory.CreateObstacle();
            _gameFactory.CreateHud();
            _gameFactory.CreateGameLoopController(playerFacade);
            _uiFactory.CreateUIRoot();
            PauseTextView pauseText = _uiFactory.CreateInitialPauseText();
            pauseText.StartAnimation();
        }
    }
}