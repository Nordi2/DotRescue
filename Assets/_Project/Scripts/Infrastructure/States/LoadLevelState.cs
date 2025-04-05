using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Factory;
using DG.Tweening;

namespace _Project.Scripts.Infrastructure.States
{
    public class LoadLevelState :
        IPayloadedState<string>
    {
        private IGameOverEvent _gameOverEvent;

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
            InitUI();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            InitPauseText();
            InitPopupScoring();
            
            void InitPauseText()
            {
                PauseTextView pauseText = _uiFactory.CreateInitialPauseText();
                pauseText.StartAnimation();
            }
            void InitPopupScoring()
            {
                PopupScoringView popupScoring = _uiFactory.CreatePopupScoring(_gameOverEvent, _gameFactory.StorageScore);
                popupScoring.gameObject.SetActive(false);
            }
        }

        private void InitGameWorld()
        {
            _gameFactory.CreatePlayArea();
            _gameFactory.CreateObstacle();
            _gameFactory.CreateHud();
            _gameOverEvent = _gameFactory.CreatePlayer();
            _gameFactory.CreateGameLoopController(_gameOverEvent);
        }
    }
}