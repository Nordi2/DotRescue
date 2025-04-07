using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using DG.Tweening;

namespace _Project.Scripts.Infrastructure.States
{
    public class LoadLevelState :
        IPayloadedState<string>
    {
        private IGameOverEvent _gameOverEvent;
        private IInputService _inputService;
        private IGameLoopService _gameLoopService;
        
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
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            DOTween.Init();
            InitGameService();
            InitGameWorld();
            InitUI();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameService()
        {
            _inputService = _gameFactory.CreateInputService();
            _gameLoopService = _gameFactory.CreateGameLoopService();
            
            _gameLoopService.AddListener(_inputService);
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            InitPauseText();
            InitPopupScoring();

            void InitPauseText()
            {
                PauseTextView pauseText = _uiFactory.CreateInitialPauseText(_inputService,_gameLoopService);
                pauseText.StartAnimation();
            }

            void InitPopupScoring()
            {
                PopupScoringView popupScoring =
                    _uiFactory.CreatePopupScoring(_gameOverEvent, _gameFactory.StorageScore,_gameLoopService);
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